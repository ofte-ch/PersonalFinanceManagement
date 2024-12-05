import { useMemo } from "react";
import { Button, message, Popconfirm, Space } from "antd";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import { useDeleteAccountTypes } from "~/api/account-types/delete-account-types";
import { useAccountTypesStore } from "~/stores/account-types/accountTypesStore";
import { useAccountTypes } from "~/api/account-types/get-account-types";
const useAccountTypesColumn = () => {
  const { setOpenUpdateModal, setAccountType } = useAccountTypesStore((state) => state);
  const { data: accountTypes } = useAccountTypes();
  const mutateDelete = useDeleteAccountTypes({
    onSuccess: () => {
      message.success("Delete account types successfully");
    },
    onError: (error) => {
      message.error(`Delete account types failed. Reason: ${error.message}`);
    },
  });
  const getAccountType = (id) => {
    const accountType = accountTypes?.find(type => type.id === id);
    return accountType ? accountType.ten : "Not exist";
  }
  const handleEdit = (accountType) => {
    setAccountType(account);
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
      title: "Account type name",
      dataIndex: "tenLoaiTaiKhoan",
      key: "tenLoaiTaiKhoan",
    },
    {
      title: "Action",
      key: "action",
      render: (_, account) => (
        <Space>
          <Button onClick={() => handleEdit(account)} icon={<EditOutlined />} />
          <Popconfirm
            title="Delete the account type"
            description="Are you sure to delete this account type?"
            onConfirm={() => handleDelete(account.id)}
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

export default useAccountTypesColumn;
