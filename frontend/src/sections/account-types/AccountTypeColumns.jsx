import { useMemo } from "react";
import { Button, message, Popconfirm, Space } from "antd";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import { useDeleteAccountType } from "~/api/account-types/delete-account-type";
import { useAccountTypeStore } from "~/stores/account-types/accountTypeStore";

const useAccountTypeColumns = () => {
  const { setOpenUpdateModal, setAccountType } = useAccountTypeStore((state) => state);
  
  const mutateDelete = useDeleteAccountType({
    onSuccess: () => {
      message.success("Delete account type successfully");
    },
    onError: (error) => {
      message.error(`Delete account type failed. Reason: ${error.message}`);
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
      title: "ID",
      dataIndex: "id",
      key: "Id",
      align: "center",
    },
    {
      title: "Account type name",
      dataIndex: "ten",
      key: "Ten",
      align: "center",
    },
    {
      title: "Action",
      key: "action",
      align: "center",
      render: (_, accountType) => (
        <Space>
          <Button onClick={() => handleEdit(accountType)} icon={<EditOutlined />} />
          <Popconfirm
            title="Delete the account type"
            description="Are you sure to delete this account type?"
            onConfirm={() => handleDelete(accountType.id)}
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

export default useAccountTypeColumns;
