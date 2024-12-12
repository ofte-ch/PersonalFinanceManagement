import { PlusSquareOutlined } from "@ant-design/icons";
import { Button, Flex, Space } from "antd";
import PageHeader from "~/components/page-header";
import ConfirmModal from "~/components/modal/ConfirmModal";
import { useAccountStore } from "~/stores/accounts/accountStore";
import { AccountTable } from "~/sections/accounts/AccountTable";
import CreateAccountModal from "~/sections/accounts/CreateAccountModal";
import UpdateAccountModal from "~/sections/accounts/UpdateAccountModal";


const AccountPage = () => {
  const {
    openDeleteModal,
    openCreateModal,
    openUpdateModal,
    setOpenDeleteModal,
    setOpenCreateModal,
    setOpenUpdateModal,
  } = useAccountStore((state) => state);
  const handleDeleteCancel = () => {
    setOpenDeleteModal(false);
  };

  const handleCreate = () => {
    setOpenCreateModal(true);
  };

  const handleCreateCancel = () => {
    setOpenCreateModal(false);
  };

  const handleUpdateCancel = () => {
    setOpenUpdateModal(false);
  };
  return (
    <>
      <Flex align="center" justify="space-between" className="mb-2">
        <PageHeader
          heading={("Tài khoản")}
          links={[
            { title: ("Tổng quan"), href: "/dashboard" },
            { title: ("Tài khoản") },
          ]}
        />
        <Space>
          <Button
            onClick={handleCreate}
            type="primary"
            icon={<PlusSquareOutlined />}
          >
            Thêm
          </Button>
        </Space>
      </Flex>
      <div style={{ paddingTop: 20 }}>
        <AccountTable />
      </div>
      <ConfirmModal
        title={'("warning_delete.Account")'}
        content={"Coming Soon"}
        open={openDeleteModal}
        handleCancel={handleDeleteCancel}
        handleOk={() => {}}
      />
      <CreateAccountModal
        open={openCreateModal}
        handleCancel={handleCreateCancel}
      />
      <UpdateAccountModal
        open={openUpdateModal}
        handleCancel={handleUpdateCancel}
        selectedAccount={null}
      />
    </>
  );
};
export default AccountPage;
