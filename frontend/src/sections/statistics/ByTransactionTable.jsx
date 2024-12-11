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
            title: 'Loại',
            dataIndex: 'tenTheLoai',
            key: 'tenTheLoai',
        },
        {
            title: 'Tổng thu',
            dataIndex: 'tongThu',
            key: 'tongThu',
            render: (text) => `${text} VND`,
        },
        {
            title: 'Số GD thu',
            dataIndex: 'soLuongGiaoDichThu',
            key: 'soLuongGiaoDichThu',
        },
        {
            title: 'Tổng chi',
            dataIndex: 'tongChi',
            key: 'tongChi',
            render: (text) => `${text} VND`,
        },
        {
            title: 'Số GD chi',
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
