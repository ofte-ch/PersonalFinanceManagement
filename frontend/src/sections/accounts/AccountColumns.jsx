import { useMemo } from "react";
import { Button, message, Popconfirm, Space } from "antd";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import { useDeleteAccount } from "~/api/accounts/delete-account";
import { useAccountStore } from "~/stores/accounts/accountStore";
import { useAccountTypes } from "~/api/account-types/get-account-types";
const useAccountColumn = () => {
  const { setOpenUpdateModal, setAccount } = useAccountStore((state) => state);
  const { data: accountTypes } = useAccountTypes();
  const mutateDelete = useDeleteAccount({
    onSuccess: () => {
      message.success("Delete account successfully");
    },
    onError: (error) => {
      message.error(`Delete account failed. Reason: ${error.message}`);
    },
  });
  const getAccountType = (id) => {
    const accountType = accountTypes?.find(type => type.id === id);
    return accountType ? accountType.ten : "Not exist";
  }
  const handleEdit = (account) => {
    setAccount(account);
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
      title: "Account name",
      dataIndex: "tenTaiKhoan",
      key: "tenTaiKhoan",
    },
    {
      title: "Account type",
      dataIndex: "loaiTaiKhoanId",
      key: "loaiTaiKhoanId",
      render : (loaiTaiKhoanId) => getAccountType(loaiTaiKhoanId),
    },
    {
      title: "Balance",
      dataIndex: "soDu",
      key: "soDu",
      render: (text) => `${text.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',')} VND`,
    },
    {
      title: "Action",
      key: "action",
      render: (_, account) => (
        <Space>
          <Button onClick={() => handleEdit(account)} icon={<EditOutlined />} />
          <Popconfirm
            title="Delete the account"
            description="Are you sure to delete this account?"
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

export default useAccountColumn;
