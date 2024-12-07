import { Button, Flex } from "antd/lib";
import React, { useState, useEffect, useMemo } from "react";
import TransactionsTable from "~/sections/transactions/TransactionsTable"
import AddNewTransactionModal from "~/sections/transactions/CreateModal";
import UpdateTransactionModal from "~/sections/transactions/UpdateModal";
import PageHeader from "~/components/page-header";
import { getAllAccounts } from "~/api/accounts/get-accounts";
import { useTransactionStore } from "~/stores/transactions/transactionStore";

const TransactionsPage = () =>{
    const {
        openDeleteModal,
        openCreateModal,
        openUpdateModal,
        setOpenCreateModal,
        setOpenUpdateModal,
    } = useTransactionStore((state) => state);

    console.log(accountList);
    return (
        <>
        <Flex gap="middle" justify="space-between" className="mb-2">
            <PageHeader
            heading={("Transactions")}
            links={[
                { title: ("Dashboard"), href: "/dashboard" },
                { title: ("Transactions")},
            ]}
            />
            <Button 
                className="bg-gradient-to-r from-teal-400 to-blue-500 mt-3 w-[80px] h-[50px]" 
                onClick={() => setOpenCreateModal(true)}
                fontWeight="700"
            >
                New +
            </Button>
        </Flex>
        
        <TransactionsTable/>
                        
        <AddNewTransactionModal 
            open={openCreateModal}
            handleCancel={() => {setOpenCreateModal(false)}}/>
        <UpdateTransactionModal 
            open={openUpdateModal} 
            handleCancel={() => {setOpenUpdateModal(false)}}
            selectedTransaction={null}/>
        </>
    )
}
export default TransactionsPage;