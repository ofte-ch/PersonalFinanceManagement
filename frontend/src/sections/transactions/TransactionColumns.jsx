import { useMemo } from "react";
import { Button, message, Popconfirm, Space } from "antd";
import { DeleteOutlined, EditOutlined,InfoCircleOutlined } from "@ant-design/icons";
import { useDeleteTransaction } from "~/api/transactions/delete-transaction";
import { useTransactionStore } from "~/stores/transactions/transactionStore";
import { useAccountTypes } from "~/api/account-types/get-account-types";
import moment from "moment";
const useTransactionColumn = () => {
  const { setOpenUpdateModal, setTransaction } = useTransactionStore(
    (state) => state
  );
  const { data: accountTypes } = useAccountTypes();
  const mutateDelete = useDeleteTransaction({
    onSuccess: () => {
      message.success("Delete transaction successfully");
    },
    onError: (error) => {
      message.error(`Delete transaction failed. Reason: ${error.message}`);
    },
  });
  const getAccountType = (id) => {
    const accountType = accountTypes?.find((type) => type.id === id);
    return accountType ? accountType.ten : "Not exist";
  };
  const handleEdit = (transaction) => {
    const formattedTransaction = {
      ...transaction,
      ngayGiaoDich: moment(transaction.ngayGiaoDich).format("YYYY-MM-DD HH:mm:ss"),
    }
    setTransaction(formattedTransaction);
    setOpenUpdateModal(true);
  };

  const handleDelete = (id) => {
    mutateDelete.mutate(id);
  };
  return useMemo(() => [
    {
      title: "ID",
      dataIndex: "id",
      key: "id",
      align: "center",
    },
    {
      title: "Name",
      dataIndex: "tenGiaoDich",
      key: "tenGiaoDich",
    },
    {
      title: "Date",
      dataIndex: "ngayGiaoDich",
      key: "ngayGiaoDich",
      render: (ngayGiaoDich) => moment(ngayGiaoDich).format("YYYY-MM-DD HH:mm:ss"),
    },
    {
      title: "Type",
      dataIndex: "loaiGiaoDich",
      key: "loaiGiaoDich",
    },
    {
      title: "Total (VND)",
      dataIndex: "tongTien",
      key: "tongTien",
      render: (text) => `${text} VND`,
    },
    {
      title: "Note",
      dataIndex: "ghiChu",
      key: "ghiChu",
    },
    {
      title: "Action",
      key: "action",
      render: (_, transaction) => (
        <Space>
          <Button onClick={() => handleEdit(transaction)} icon={<InfoCircleOutlined />} />
          <Popconfirm
            title="Delete the account"
            description="Are you sure to delete this account?"
            onConfirm={() => handleDelete(transaction.id)}
            okText="Yes"
            cancelText="No"
          >
            <Button danger icon={<DeleteOutlined />} />
          </Popconfirm>
        </Space>
      ),
    },
  ]);
};

export default useTransactionColumn;
