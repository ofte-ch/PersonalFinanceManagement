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
            title: 'Ten the loai',
            dataIndex: 'tenTheLoai',
            key: 'tenTheLoai',
        },
        {
            title: 'Tong thu',
            dataIndex: 'tongThu',
            key: 'tongThu',
            render: (text) => `${text} VND`,
        },
        {
            title: 'So luong giao dich thu',
            dataIndex: 'soLuongGiaoDichThu',
            key: 'soLuongGiaoDichThu',
        },
        {
            title: 'Tong chi',
            dataIndex: 'tongChi',
            key: 'tongChi',
            render: (text) => `${text} VND`,
        },
        {
            title: 'So luong giao dich chi',
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
