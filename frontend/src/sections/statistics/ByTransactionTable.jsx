import { Table } from 'antd';
import PropTypes from 'prop-types';


export const ByTransactionTable = ({ data }) => {
    
    const columns = [
        {
            title: 'ID',
            dataIndex: 'theLoaiId',
            key: 'theLoaiId',
            
        },
        {
            title: 'Type name',
            dataIndex: 'tenTheLoai',
            key: 'tenTheLoai',
        },
        {
            title: 'Total receipts',
            dataIndex: 'tongThu',
            key: 'tongThu',
            render: (text) => `${text} VND`,
        },
        {
            title: 'Receipts quantity',
            dataIndex: 'soLuongGiaoDichThu',
            key: 'soLuongGiaoDichThu',
        },
        {
            title: 'Total expenditures',
            dataIndex: 'tongChi',
            key: 'tongChi',
            render: (text) => `${text} VND`,
        },
        {
            title: 'Expenditures quantity',
            dataIndex: 'soLuongGiaoDichChi',
            key: 'soLuongGiaoDichChi',
        },
    ]

    return (
        <Table
            columns={columns}
            dataSource={data}
            size="middle"
            rowKey={(record) => record.theLoaiId}
           
        />
    )

}

ByTransactionTable.propTypes = {
    data: PropTypes.array.isRequired,
};
