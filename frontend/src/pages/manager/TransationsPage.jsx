import { Table, Button } from "antd/lib";
import React, { useEffect, useState } from "react";
import { Modal } from "antd";

const TransactionsPage = () =>{
    const [openModal, setOpenModal] = useState(false);
    const [columns, setColumns] = useState([
        {
            title: "ID",
            dataIndex: "id"
        },
        {
            title: "Name",
            dataIndex: "tenGiaoDich"
        },
        {
            title: "Date",
            dataIndex: "ngayGiaoDich"
        },
        {
            title: "Type",
            dataIndex: "loaiGiaoDich"
        },
        {
            title: "Total",
            dataIndex: "tongTien"
        },
        {
            title: "Note",
            dataIndex: "ghiChu"
        }
    ]);
    // Get dữ liệu giao dịch
    const [dataSource, setDataSource] = useState([]);
    useEffect( ()=> {
        fetch("https://localhost:44318/api/v1/GiaoDich")
            .then((res) => res.json())
            .then((result) => {
                setDataSource(result);
                console.log(dataSource)
            })
    }, []);

    // Mở modal thêm giao dịch
    const handleOpenModal = () => {
        setOpenModal(true);
    }
    // Đóng modal thêm giao dịch
    const handleCloseModal = () => {
        setOpenModal(false);
    }


    return (
        <>
        <div className="flex justify-between">
            <Button 
                className="bg-gradient-to-r from-teal-400 to-blue-500 hover:from-pink-500 hover:to-orange-500" onClick={handleOpenModal}
                >
                New +
            </Button>
            <button type="button" className="bg-gradient-to-r from-teal-400 to-blue-500 hover:from-pink-500 hover:to-orange-500 ..."> New + (2)</button>
        </div>

        <Modal title="Add new transaction" open={openModal} maskClosable={false} onOk={handleCloseModal} onCancel={handleCloseModal}> 
            <label htmlFor="name">Name</label>
            <input id="name" type="text" />
        </Modal>
        <Table columns={columns} dataSource={dataSource} rowKey="id"></Table>
        </>
    )
}

export default TransactionsPage;