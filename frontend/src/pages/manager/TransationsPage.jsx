import { Table, Typography } from "antd/lib";
import React, { useEffect, useState } from "react";

const TransactionsPage = () =>{
    
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
    const [dataSource, setDataSource] = useState([]);
    useEffect( ()=> {
        fetch("https://localhost:44318/api/v1/GiaoDich")
            .then((res) => res.json())
            .then((result) => {
                setDataSource(result);
            })
    }, []);

    return (
        <>
            <Table columns={columns} dataSource={dataSource}></Table>
        </>
    )
}

export default TransactionsPage;