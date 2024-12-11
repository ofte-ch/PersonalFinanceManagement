import {Table, Typography} from 'antd';
import PropTypes from 'prop-types';
import moment from 'moment';

const { Text } = Typography;


export const TransactionTable = ({ data }) => {
    const transactionColumns = [
        {
          title: "Name",
          dataIndex: "tenGiaoDich",
          key: "tenGiaoDich",
          render: (text) => <Text>{text}</Text>,
        },
        {
          title: "Date",
          dataIndex: "ngayGiaoDich",
          key: "ngayGiaoDich",
          render: (date) => <Text>{moment(date).format("YYYY-MM-DD")}</Text>,
        },
        {
          title: "Total",
          dataIndex: "tongTien",
          key: "tongTien",
          render: (amount) => (
            <Text>{`${amount
              .toString()
              .replace(/\B(?=(\d{3})+(?!\d))/g, ",")} VND`}</Text>
          ),
        },
    ];

    return (
        <Table
        columns={transactionColumns}
        dataSource={data}
        size="middle"
        rowKey={(record) => record.id}
        />
    )
}

TransactionTable.propTypes = {
    data: PropTypes.array.isRequired,
}