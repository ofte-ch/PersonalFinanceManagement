import { PlusSquareOutlined } from "@ant-design/icons";
import { Button, Flex, Space } from "antd";
import PageHeader from "~/components/page-header";
import ConfirmModal from "~/components/modal/ConfirmModal";
import { useAccountTypeStore } from "~/stores/account-types/accountTypeStore";
import { AccountTypeTable } from "~/sections/account-types/AccountTypeTable";
import CreateAccountTypeModal from "~/sections/account-types/CreateAccountTypeModal";
import UpdateAccountTypeModal from "~/sections/account-types/UpdateAccountTypeModal";


const AccountTypePage = () => {
  const {
    openDeleteModal,
    openCreateModal,
    openUpdateModal,
    setOpenDeleteModal,
    setOpenCreateModal,
    setOpenUpdateModal,
  } = useAccountTypeStore((state) => state);
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
          heading={("Account Types")}
          links={[
            { title: ("Dashboard"), href: "/dashboard" },
            { title: ("Account Types") },
          ]}
        />
        <Space>
          <Button
            onClick={handleCreate}
            type="primary"
            icon={<PlusSquareOutlined />}
          >
            Add
          </Button>
        </Space>
      </Flex>
      <div style={{ paddingTop: 20 }}>
        <AccountTypeTable />
      </div>
      <ConfirmModal
        title={'("warning_delete.Account")'}
        content={"Coming Soon"}
        open={openDeleteModal}
        handleCancel={handleDeleteCancel}
        handleOk={() => {}}
      />
      <CreateAccountTypeModal
        open={openCreateModal}
        handleCancel={handleCreateCancel}
      />
      <UpdateAccountTypeModal
        open={openUpdateModal}
        handleCancel={handleUpdateCancel}
        selectedAccount={null}
      />
    </>
  );
};
export default AccountTypePage;
