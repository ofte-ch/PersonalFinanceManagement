import React, {useEffect, useMemo, useState} from "react";
import { Table, Button, Space, Input, Select } from "antd/lib";
import { DeleteOutlined, DeleteFilled, InfoCircleOutlined, InfoCircleFilled } from "@ant-design/icons"
import { useGetTransactions } from "../../api/transactions/get-transactions";
import { useDeleteTransaction } from "~/api/transactions/delete-transaction";
import { useTransactionStore } from "~/stores/transactions/transactionStore";

const { Option } = Select;
const size = 8;

const TransactionsTable = ({accountList}) => {
    //const [selectedAccount, setSelectedAccount] = useState("All");
    const [page, setPage] = useState(1);
    const [keyword, setKeyword] = useState("");
    const [maTaiKhoan, setMaTaiKhoan] = useState("");
    const [selectedAccount, setSelectedAccount] = useState("All");
    const mutationDelete = useDeleteTransaction();

    const {setOpenUpdateModal, setTransaction} = useTransactionStore();

    const {data:data, isLoading} = useGetTransactions({page, size, keyword, maTaiKhoan});

    const handleEdit = (transaction) => {
        setTransaction(transaction);
        setOpenUpdateModal(true);
    }
    const handleDelete = (id) => {
        const result = alert("Are you sure");
        mutationDelete.mutate(id);
    }

    // Các cột trong bảng
    const columns = useMemo( () => [
        {
            title: "STT",
            dataIndex: "stt",
            key:"index",
            align:"center",
            width:"2%",
            render: (text, record, index) => (page - 1) * size + index + 1,
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
                            handleDelete(giaoDich.id);
                        }}
                    /> 
                </Space>
            )
        }
    ]);
    return (
        <>
        <Table 
            className="bg-panel border-2 "
            columns={columns}
            dataSource={data?.data} 
            rowClassName="bg-panel text-elements-primary border-2 border-border"
            rowHoverable={false}
            rowKey="id"
            size="middle"
            pagination={{
                className:"bg-elements text-elements-secondary",
                current:page,
                pageSize:size,
                total:data?.totalCount,
                onChange: (p) => {setPage(p)}
            }}
            bordered
            loading={isLoading}
            title={() => (
                <Space className="filter-container" align="center">
                  <Input.Search
                    className="filter-item search-input"
                    placeholder="Search transactions by name ..."
                    onSearch={(value) =>{
                        setKeyword(value.trim());
                        setPage(1);
                    }}
                  />
                  <Space className="filter-item" align="center">
                    <label className="selector-account-label">Account: </label>
                    <Select
                        className="select-account"
                        placeholder="Choose account"
                        value={selectedAccount}
                        onChange={(option) => {
                            setSelectedAccount(option);
                            setMaTaiKhoan(option === "All" ? "" : 
                                            accountList.find(tk => tk.id === option)?.id || "");
                            setPage(1);
                        }}
                    >
                      <Option key="0" value="All">All</Option>
                      {accountList && accountList.map((account) => (
                        <Option key={account.id} value={account.id}>
                          {account.tenTaiKhoan}
                        </Option>
                      ))}
                    </Select>
                  </Space>
                </Space>
                )}
            scroll={{ x: 600 }}
            >
        </Table>
        </>
    )
}
export default TransactionsTable;