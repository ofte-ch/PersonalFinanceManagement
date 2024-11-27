import { Table } from 'antd';
import PropTypes from 'prop-types';


export const ByAccountTable = ({ data }) => {
    
    const columns = [
        {
            title: 'ID',
            dataIndex: 'taiKhoanId',
            key: 'taiKhoanId',
            
        },
        {
            title: 'Account name',
            dataIndex: 'tenTaiKhoan',
            key: 'tenTaiKhoan',
        },
        {
            title: 'Account type',
            dataIndex: 'loaiTaiKhoan',
            key: 'loaiTaiKhoan',
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
            rowKey={(record) => record.taiKhoanId}
           
        />
    )

}

ByAccountTable.propTypes = {
    data: PropTypes.array.isRequired,
};
