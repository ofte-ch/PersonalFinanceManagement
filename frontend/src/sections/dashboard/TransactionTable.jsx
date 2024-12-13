import {Table, Typography} from 'antd';
import PropTypes from 'prop-types';
import moment from 'moment';

const { Text } = Typography;


export const TransactionTable = ({ data }) => {
    const transactionColumns = [
        {
          title: "Tên",
          dataIndex: "tenGiaoDich",
          key: "tenGiaoDich",
          render: (text) => <Text>{text}</Text>,
        },
        {
          title: "Ngày GD",
          dataIndex: "ngayGiaoDich",
          key: "ngayGiaoDich",
          render: (date) => <Text>{moment(date).format("YYYY-MM-DD")}</Text>,
        },
        {
          title: "Tổng tiền(VND)",
          dataIndex: "tongTien",
          key: "tongTien",
          render: (amount) => (
            <Text>{`${amount
              .toString()
              .replace(/\B(?=(\d{3})+(?!\d))/g, ",")}`}</Text>
          ),
        },
    ];

    return (
        <Table
        columns={transactionColumns}
        dataSource={data}
        size="middle"
        rowKey={(record) => record.id}
        scroll={{ x:400 }}
        pagination={false}
        />
    )
}

TransactionTable.propTypes = {
    data: PropTypes.array.isRequired,
}