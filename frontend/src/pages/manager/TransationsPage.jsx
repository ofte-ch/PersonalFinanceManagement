import { Button } from "antd/lib";
import React, { useState } from "react";
import TransactionsTable from "~/sections/transactions/TransactionsTable"
import AddNewTransactionModal from "~/sections/transactions/CreateModal";


const TransactionsPage = () =>{
    const [openAddingModal, setOpenAddingModal] = useState(true);
    const [currentMaxId, setCurrentMaxId] = useState([]);
    // Mở modal thêm giao dịch
    const handleOpenModal = () => {
        setOpenAddingModal(true);
    }

    return (
        <>
            <Button 
                className="bg-gradient-to-r from-teal-400 to-blue-500 hover:from-pink-500 hover:to-orange-500" onClick={handleOpenModal}
                >
                New +
            </Button>
            <button type="button" className="bg-gradient-to-r from-teal-400 to-blue-500 hover:from-pink-500 hover:to-orange-500 ..."> New + (2)</button>
        </div>

        <TransactionsTable/>
        <AddNewTransactionModal currentMaxId={currentMaxId} isOpened={openAddingModal} setOpenAddingModal={setOpenAddingModal}/>
        </>
    )
}

export default TransactionsPage;