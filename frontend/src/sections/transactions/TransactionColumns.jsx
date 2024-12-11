import { useMemo } from "react";
import { Button, message, Popconfirm, Space } from "antd";
import { DeleteOutlined, EditOutlined,InfoCircleOutlined } from "@ant-design/icons";
import { useDeleteTransaction } from "~/api/transactions/delete-transaction";
import { useTransactionStore } from "~/stores/transactions/transactionStore";
import { useAccountTypes } from "~/api/account-types/get-account-types";
import moment from "moment";
const useTransactionColumn = (currentPage, pageSize) => {
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
    setTransaction(transaction);
    setOpenUpdateModal(true);
  };

  const handleDelete = (id) => {
    mutateDelete.mutate(id);
  };
  return useMemo(() => [
    {
      title: "STT",
      dataIndex: "stt",
      key: "stt",
      align: "center",
      width:"5%",
      render: (text, record, index) => (currentPage-1) * pageSize + index + 1
    },
    {
      title: "Tên",
      dataIndex: "tenGiaoDich",
      key: "tenGiaoDich",
      width:"20%",
    },
    {
      title: "Ngày GD",
      dataIndex: "ngayGiaoDich",
      key: "ngayGiaoDich",
      render: (ngayGiaoDich) => moment(ngayGiaoDich).format("YYYY-MM-DD"),
      width:"15%",
    },
    {
      title: "Thể loai",
      dataIndex: "theLoai",
      key: "theLoai",
      width:"10%",
      render: (theLoai) => theLoai.tenTheLoai
    },
    {
      title: "Tổng tiền(VND)",
      dataIndex: "tongTien",
      key: "tongTien",
      width:"15%",
      render: (text) => `${text.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',')}` ,
    },
    {
      title: "Ghi chú",
      dataIndex: "ghiChu",
      key: "ghiChu",
      width:"25%",
    },
    {
      title: "Thao tác",
      key: "action",
      width:"10%",
      render: (_, transaction) => (
        <Space>
          <Button onClick={() => handleEdit(transaction)} icon={<InfoCircleOutlined />} />
          <Popconfirm
            title="Delete the transaction"
            description="Are you sure to delete this transaction?"
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
