import React, {useEffect, useMemo, useState} from "react";
import { Table, Button, Space, Input, Select } from "antd/lib";
import { DeleteOutlined, DeleteFilled, InfoCircleOutlined, InfoCircleFilled } from "@ant-design/icons"
import { useGetTransactions } from "../../api/transactions/get-transactions";

const { Option } = Select;
const size = 8;

const TransactionsTable = (
    {accountList, setOpenDeleteConfirmDialog,setOpenUpdateModal,
    setSelectedTransaction, setCurrentMaxId}) => {
    //const [selectedAccount, setSelectedAccount] = useState("All");
    const [page, setPage] = useState(1);
    const [keyword, setKeyword] = useState("");
    const [codeTK, setCodeTK] = useState("");
    const [selectedAccount, setSelectedAccount] = useState("All");

    const {data:data, isLoading} = useGetTransactions({page, size, keyword, codeTK});

    const handleEdit = (transaction) => {
        setOpenUpdateModal(true);
        setSelectedTransaction(transaction);
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
                    onSearch={(value) => console.log(value)}
                  />
                  <Space className="filter-item" align="center">
                    <label className="selector-account-label">Account: </label>
                    <Select
                        className="select-account"
                        placeholder="Choose account"
                        value={selectedAccount}
                        onChange={(option) => setSelectedAccount(option)}
                    >
                      <Option key="0" value="All">All</Option>
                      {accountList.map((account) => (
                        <Option key={account.id} value={account.tenTaiKhoan}>
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