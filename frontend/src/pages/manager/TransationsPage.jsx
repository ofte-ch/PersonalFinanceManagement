import { Button, Flex } from "antd/lib";
import React, { useState, useEffect, useMemo } from "react";
import TransactionsTable from "~/sections/transactions/TransactionsTable"
import AddNewTransactionModal from "~/sections/transactions/CreateModal";
import UpdateTransactionModal from "~/sections/transactions/UpdateModal";
import PageHeader from "~/components/page-header";
import { getAllAccounts } from "~/api/accounts/get-accounts";


const TransactionsPage = () =>{
    // All modal and dialog states
    const [openAddingModal, setOpenAddingModal] = useState(false);
    const [openUpdateModal, setOpenUpdateModal] = useState(false);
    const [openDeleteConfirmDialog, setOpenDeleteConfirmDialog] = useState(false);
    // Current max ID, increase 1 unit when a new transaction's been created
    const [currentMaxId, setCurrentMaxId] = useState([]);
    // Selected transaction to view detail and edit
    const [selectedTransaction, setSelectedTransaction] = useState(undefined);
    // Accountlst
    const [accountList, setAccountList] = useState([]);

    useEffect(() => {
        getAllAccounts()
            .then( res => {
                setAccountList(res);
            })
            .catch( error => alert(error));
      }, []);

    const memoizedAccounts = useMemo(() => accountList, [accountList]);

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
                onClick={() => setOpenAddingModal(true)}
                fontWeight="700"
            >
                New +
            </Button>
        </Flex>
        
        <TransactionsTable accountList={accountList} setOpenDeleteConfirmDialog={setOpenDeleteConfirmDialog} setOpenUpdateModal={setOpenUpdateModal} setSelectedTransaction={setSelectedTransaction}/>

        <AddNewTransactionModal currentMaxId={currentMaxId} setCurrentMaxId={setCurrentMaxId} isOpened={openAddingModal} setOpenAddingModal={setOpenAddingModal}/>
        <UpdateTransactionModal transaction={selectedTransaction} setSelectedTransaction={setSelectedTransaction} isOpened={openUpdateModal} setOpenUpdateModal={setOpenUpdateModal} />
        </>
    )
}
export default TransactionsPage;