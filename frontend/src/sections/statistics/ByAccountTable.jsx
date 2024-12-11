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
            title: 'Tên TK',
            dataIndex: 'tenTaiKhoan',
            key: 'tenTaiKhoan',
        },
        {
            title: 'Loại TK',
            dataIndex: 'loaiTaiKhoan',
            key: 'loaiTaiKhoan',
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
            rowKey={(record) => record.taiKhoanId}
           
        />
    )

}

ByAccountTable.propTypes = {
    data: PropTypes.array.isRequired,
};
