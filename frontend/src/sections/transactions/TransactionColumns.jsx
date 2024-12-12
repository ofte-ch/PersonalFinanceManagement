import { useMemo } from "react";
import { Button, message, Popconfirm, Space } from "antd";
import { DeleteOutlined, EditOutlined,InfoCircleOutlined } from "@ant-design/icons";
import { useDeleteTransaction } from "~/api/transactions/delete-transaction";
import { useTransactionStore } from "~/stores/transactions/transactionStore";
import moment from "moment";
const useTransactionColumn = (currentPage, pageSize) => {
  const { setOpenUpdateModal, setTransaction } = useTransactionStore(
    (state) => state
  );

  const mutateDelete = useDeleteTransaction({
    onSuccess: () => {
      message.success("Xóa giao dịch thành công !");
    },
    onError: (error) => {
      message.error(`Xóa giao dịch thất bại !!!\nNguyên nhân: ${error.response.data}`);
    },
  });

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
          <Button onClick={() => handleEdit(transaction)} icon={<EditOutlined />} />
          <Popconfirm
            title="Xóa"
            description="Bạn có chắc chắc chắn XÓA giao dịch này?"
            onConfirm={() => handleDelete(transaction.id)}
            okText="CÓ"
            cancelText="KHÔNG"
          >
            <Button danger icon={<DeleteOutlined />} />
          </Popconfirm>
        </Space>
      ),
    },
  ]);
};

export default useTransactionColumn;
