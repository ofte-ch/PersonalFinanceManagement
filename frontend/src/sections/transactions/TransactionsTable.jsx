import React, {useEffect, useMemo, useState} from "react";
import { Table, Button, Space, Input, Select } from "antd/lib";
import { DeleteOutlined, DeleteFilled, InfoCircleOutlined, InfoCircleFilled } from "@ant-design/icons"
import { useGetTransactions } from "../../api/transactions/get-transactions";

const { Option } = Select;
const size = 8;

const TransactionsTable = ({setOpenUpdateModal, setOpenDeleteConfirmDialod, setSelectedTransaction}) => {
    const [page, setPage] = useState(1);
    const [transactions, setTransactions] = useState([]);
    const [selectedAccount, setSelectedAccount] = useState("All");
    const [keyword, setKeyword] = useState("");
    const [codeTK, setCodeTK] = useState("");

    const [data, isLoading] = useGetTransactions({page, size, keyword:"", codeTK:""});


    return (
        <>
        </>
    )
}
/*
    const handleEdit = (transaction) => {
        setOpenUpdateModal(true);
        setSelectedTransaction(transaction);
    }

    // Các cột trong bảng
    const columns = useMemo( () => [
        {
            title: "ID",
            dataIndex: "id",
            key:"id",
            align:"center",
            width:"2%",
            render:(text) => {
                return(
                    <span className="text-primary">{text}</span>
                )
            }
        },
        {
            title: "Name",
            dataIndex: "tenGiaoDich",
            key:"tenGiaoDich",
            width:"410",
            render:(text) => {
                return(
                    <span className="text-primary">{text}</span>
                )
            }
        },
        {
            title: "Date",
            dataIndex: "ngayGiaoDich",
            key: "ngayGiaoDich",
            width:"8%",
            render:(text) => {
                return(
                    <span className="text-primary">{text.slice(0,10)}</span>
                )
            }
        },
        {
            title: "Type",
            dataIndex: "loaiGiaoDich",
            key: "loaiGiaoDich",
            width:"5%",
            render:(text) => {
                return(
                    <span className="text-primary">{text}</span>
                )
            }
        },
        {
            title: "Total (VND)",
            dataIndex: "tongTien",
            key: "tongTien",
            width:"15%",
            render:(text) => {
                const value = `${text.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',')}`
                return(
                    <span className="text-primary">{value}</span>
                )
            }
        },
        {
            title: "Note",
            dataIndex: "ghiChu",
            key: "ghiChu",
            width:"25%",
            render:(text) => {
                return(
                    <span className="text-primary">{text}</span>
                )
            }
        },
        {
            title:"Action",
            key:"action",
            width: "5%",
            render: (_, giaoDich) => (
                <Space>
                    <Button 
                        icon={<InfoCircleOutlined/>} 
                        onClick={() => {
                            handleEdit(giaoDich);
                        }}
                    /> 
                    <Button danger icon={<DeleteOutlined/>}
                        onClick={() => {
                            console.log(giaoDich.tenGiaoDich);
                            console.log(giaoDich.id)
                        }}
                    /> 
                </Space>
            )
        }
    ]);
    return (
        <>
        <Space justify="space-between">
            <Input.Search
                className="w-[250px]"
                />
            <label>Account: </label>
            {/*
            <Select 
                value={selectedAccount} 
                onChange={(option) => setSelectedAccount(option) }
            >
                <Option key="0" value="All">All</Option>
                { accountList.map( (account) => (
                    <Option key={account.id} value={account.tenTaiKhoan}>{account.tenTaiKhoan}</Option>
                )) }
            </Select>
            */
            /*
        </Space>
        <Table 
            className="bg-panel border-2 "
            columns={columns}
            dataSource={transactions} 
            rowClassName="bg-panel text-elements-primary border-2 border-border"
            rowHoverable={false}
            rowKey="id"
            size="middle"
            pagination={{
                className:"bg-elements text-elements-secondary"
            }}
            bordered
            loading={isLoading}
            >
        </Table>
        </>
    )
    */



export default TransactionsTable