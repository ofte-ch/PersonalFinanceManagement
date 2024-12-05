import { PlusSquareOutlined } from "@ant-design/icons";
import { Button, Flex, Space } from "antd";
import PageHeader from "~/components/page-header";
import ConfirmModal from "~/components/modal/ConfirmModal";
import { useTransactionStore } from "~/stores/transactions/transactionStore";
import { TransactionTableAlt } from "~/sections/transactions/TransactionTableAlter";
import CreateTransactionModal from "~/sections/transactions/CreatTransactionModal";
import UpdateTransactionModal from "~/sections/transactions/UpdateModal";

const TransactionPageAlt = () => {
  const {
    openDeleteModal,
    openCreateModal,
    openUpdateModal,
    setOpenDeleteModal,
    setOpenCreateModal,
    setOpenUpdateModal,
  } = useTransactionStore((state) => state);
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
          heading={("Transactions")}
          links={[
            { title: ("Dashboard"), href: "/dashboard" },
            { title: ("Transactions") },
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
        <TransactionTableAlt />
      </div>
      <ConfirmModal
        title={'("warning_delete.Transaction")'}
        content={"Coming Soon"}
        open={openDeleteModal}
        handleCancel={handleDeleteCancel}
        handleOk={() => {}}
      />
      <CreateTransactionModal
        open={openCreateModal}
        handleCancel={handleCreateCancel}
      />
      <UpdateTransactionModal
        open={openUpdateModal}
        handleCancel={handleUpdateCancel}
        selectedAccount={null}
      />
    </>
  );
};
export default TransactionPageAlt;
