import { useMemo } from "react";
import { Button, message, Popconfirm, Space } from "antd";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import { useDeleteAccountType } from "~/api/account-types/delete-account-type";
import { useAccountTypeStore } from "~/stores/account-types/accountTypeStore";

const useAccountTypeColumns = (currentPage, pageSize) => {
  const { setOpenUpdateModal, setAccountType } = useAccountTypeStore((state) => state);
  
  const mutateDelete = useDeleteAccountType({
    onSuccess: () => {
      message.success("Xóa loại tài khoản thành công !");
    },
    onError: (error) => {
      message.error(`Xóa loại tài khoản thất bại !!!\nReason: ${error.message}`);
    },
  });
  
  const handleEdit = (accountType) => {
    setAccountType(accountType);
    setOpenUpdateModal(true);
  };

  const handleDelete = (id) => {
    mutateDelete.mutate(id);
  };
  return useMemo(() => [
    {
      title: "STT",
      align: "center",
      render: (text, record, index) => (currentPage-1) * pageSize + index + 1
    },
    {
      title: "Tên loại TK",
      dataIndex: "ten",
      key: "Ten",
      align: "center",
    },
    {
      title: "Thao tác",
      key: "action",
      align: "center",
      render: (_, accountType) => (
        <Space>
          <Button onClick={() => handleEdit(accountType)} icon={<EditOutlined />} />
          <Popconfirm
            title="Xóa"
            description="Bạn có chắc chắc chắn XÓA loại tài khoản này ?"
            onConfirm={() => handleDelete(accountType.id)}
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

export default useAccountTypeColumns;
