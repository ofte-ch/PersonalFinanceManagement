import React, {useEffect, useState} from "react";
import { Table, Button, Space } from "antd/lib";
import { DeleteOutlined, DeleteFilled, InfoCircleOutlined, InfoCircleFilled } from "@ant-design/icons"
import { getAllTransactions }  from "~/api/transactions/get";


const TransactionsTable = () => {
    const [dataSource, setDataSource] = useState([]);

    // Get dữ liệu giao dịch
    useEffect( () => {
        getAllTransactions()
            .then(res => {
                if(res != [] || res != null || res != undefined)
                    setDataSource(res);
                else
                    console.log("!!! No Data !!!");
            })
            .catch(error => console.log(error));
    }, [])

    // Các cột trong bảng
    const columns = [
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
            render:(text) => {
                return(
                    <span className="text-primary">{text}</span>
                )
            }
        },
        {
            title: "Type",
            dataIndex: "loaiGiaoDich",
            key: "loaiGiaoDich",
            render:(text) => {
                return(
                    <span className="text-primary">{text}</span>
                )
            }
        },
        {
            title: "Total",
            dataIndex: "tongTien",
            key: "tongTien",
            render:(text) => {
                return(
                    <span className="text-primary">{text}</span>
                )
            }
        },
        {
            title: "Note",
            dataIndex: "ghiChu",
            key: "ghiChu",
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
                        icon={<InfoCircleFilled/>} 
                        onClick={() => {
                            console.log(giaoDich.id)
                        }}
                    /> 
                    <Button danger icon={<DeleteFilled/>}
                        onClick={() => {
                            console.log(giaoDich.tenGiaoDich);
                            console.log(giaoDich.id)
                        }}
                    /> 
                </Space>
            )
        }
    ]

    return (
        <Table 
            className="bg-panel border-2 "
            columns={columns}
            dataSource={dataSource} 
            rowClassName="bg-panel text-elements-primary border-2 border-border"
            rowHoverable={false}
            rowKey="id"
            size="middle"
            pagination={{
                className:"bg-elements text-elements-secondary"
            }}
            bordered
            >
        </Table>
    )
}


export default TransactionsTable