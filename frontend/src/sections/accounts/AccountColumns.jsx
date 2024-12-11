import { useMemo } from "react";
import { Button, message, Popconfirm, Space } from "antd";
import { DeleteOutlined, EditOutlined } from "@ant-design/icons";
import { useDeleteAccount } from "~/api/accounts/delete-account";
import { useAccountStore } from "~/stores/accounts/accountStore";
import { useAccountTypes } from "~/api/account-types/get-account-types";
const useAccountColumn = (currentPage, pageSize) => {
  const { setOpenUpdateModal, setAccount } = useAccountStore((state) => state);
  const { data: accountTypes } = useAccountTypes();
  const mutateDelete = useDeleteAccount({
    onSuccess: () => {
      message.success("Xóa tài khoản thành công !");
    },
    onError: (error) => {
      message.error(`Xóa tài khoản thất bại !!!\nNguyên nhân: ${error.message}`);
    },
  });
  const getAccountType = (id) => {
    const accountType = accountTypes?.find(type => type.id === id);
    return accountType ? accountType.ten : "Không tồn tại";
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
      title: "STT",
      align: "center",
      render: (text, record, index) => (currentPage-1) * pageSize + index + 1
    },
    {
      title: "Tên",
      dataIndex: "tenTaiKhoan",
      key: "tenTaiKhoan",
    },
    {
      title: "Loại TK",
      dataIndex: "loaiTaiKhoanId",
      key: "loaiTaiKhoanId",
      render : (loaiTaiKhoanId) => getAccountType(loaiTaiKhoanId),
    },
    {
      title: "Số dư",
      dataIndex: "soDu",
      key: "soDu",
      render: (text) => `${text.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',')} VND`,
    },
    {
      title: "Thao tác",
      key: "action",
      render: (_, account) => (
        <Space>
          <Button onClick={() => handleEdit(account)} icon={<EditOutlined />} />
          <Popconfirm
            title="Xóa"
            description="Bạn có chắc chắc chắn XÓA tài khoản này ?"
            onConfirm={() => handleDelete(account.id)}
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

export default useAccountColumn;
