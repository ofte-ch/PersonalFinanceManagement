import {Table, Typography} from 'antd';
import PropTypes from 'prop-types';
const { Text } = Typography;

// columns for accounts table
export const AccountTable = ({ data }) => {
    const accountColumns = [
        {
        title: "Name",
        dataIndex: "tenTaiKhoan",
        key: "tenTaiKhoan",
        render: (text) => <Text>{text}</Text>,
        },
        {
        title: "Total",
        dataIndex: "soDu",
        key: "soDu",
        render: (amount) => (
            <Text>{`${amount
            .toString()
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",")} VND`}</Text>
        ),
        },
    ];
    return (
        <Table
        columns={accountColumns}
        dataSource={data}
        size="middle"
        rowKey={(record) => record.id}
        />
    )
}

AccountTable.propTypes = {
    data: PropTypes.array.isRequired,
}