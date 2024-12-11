import React, { useEffect, useState } from "react";
import {
  Layout,
  Card,
  Row,
  Col,
  Typography,
  Tag,
} from "antd";
import { CreditCardOutlined, ArrowUpOutlined } from "@ant-design/icons";
import PageHeader from "~/components/page-header";
import { getAllAccounts } from "~/api/accounts/get-accounts";
import moment from "moment";
import { useGetTransactionsByDateRange } from "~/api/transactions/get-transaction-for-dashboard";

import { AccountTable } from "~/sections/dashboard/AccountTable";
import { TransactionTable } from "~/sections/dashboard/TransactionTable";
import { LineChartMonthly } from "~/sections/dashboard/LineChartMonthly";


import { Line } from "react-chartjs-2";
import { Chart as ChartJS, Tooltip, Legend, LineElement, CategoryScale, LinearScale, PointElement } from "chart.js";

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Tooltip, Legend);

const { Header, Content } = Layout;
const { Title, Text } = Typography;

const DashboardPage = () => {
  // Lấy tk
  const [accounts, setAccounts] = useState([]);
  useEffect(() => {
    getAllAccounts().then((accounts) => setAccounts(accounts));
  }, []);
  // Lấy giao dịch
  const { data: transactions, isLoading } = useGetTransactionsByDateRange({
    page: 1,
    size: 10,
    TuNgay: "2024-10-01 00:00:00",
    DenNgay: "2024-10-31 23:59:59",
  });


  // tinh tong thu chi theo thang
  const processTransactionData = () => {
    const monthlyData = [];

    // Khởi tạo 12 tháng trong năm
    for (let i = 0; i < 12; i++) {
        monthlyData.push({
          month: moment().month(i).format("MMMM"), // Tên tháng
          income: 0,
          expense: 0,
        });
      }
  
      transactions?.data.forEach((transaction) => {
        const month = moment(transaction.ngayGiaoDich).month(); // Lấy tháng từ ngày giao dịch

        if (transaction.theLoai.phanLoai === "Thu") {
          monthlyData[month].income += transaction.tongTien; // Thêm vào thu nhập
        } else {
          monthlyData[month].expense += transaction.tongTien; // Thêm vào chi tiêu
        }
      });
  
      return monthlyData;
  }

  // gan du lieu cho bieu do
  const dataForChart = processTransactionData();
  
  return (
    <Layout style={{ minHeight: "100vh", background: "#f5f5f5" }}>
      {/* Header */}
      <PageHeader
        heading={"Dashboard"}
        links={[{ title: "Dashboard", href: "/" }]}
      />
      {/* Content */}
      <Content style={{ padding: "0 24px", marginTop: "24px" }}>
        {/*Dòng đầu gồm : 
            Biểu đồ giao dịch trong tháng vừa qua 
            Danh sách các tài khoản
      */}
        <Row gutter={[16, 16]}>
        
          {/* Monthly Earnings */}
          <Col span={12}>
            <Card style={{ borderRadius: "12px" }}>
              <Title level={5}>Hàng tháng</Title>

              {/* Biểu đồ */}
              <div style={{ textAlign: "center" }}>
              {/* <Line data={chartData} options={chartOptions} /> */}
                <LineChartMonthly transactions={dataForChart} />

              </div>
            </Card>
          </Col>

          {/* Danh sách các tài khoản */}
          <Col span={12}>
            <Card style={{ borderRadius: "12px" }}>
              <Title level={5}>Tài khoản</Title>
              {/* <Table
                columns={accountColumns}
                dataSource={accounts}
                pagination={false}
                rowKey={(record) => record.id}
              /> */}
              <AccountTable data={accounts} />

            </Card>
          </Col>
          
        </Row>
        {/*Dòng 2 bao gồm:
            + Danh sách giao dịch
            + Tổng tiền đang sở hữu + thu nhập + chi tiêu)
        */}
        <Row gutter={[16, 16]} style={{marginTop: "24px"}}>
          {/* Danh sách các giao dịch */}
          <Col span={12}>
            <Card style={{ borderRadius: "12px" }}>
              <Title level={5}>Giao dịch</Title>
              {/* <Table
                columns={transactionColumns}
                dataSource={transactions?.data}
                pagination={false}
                rowKey={(record) => record.id}
               
              /> */}
            <TransactionTable data={transactions?.data} />
              
            
            </Card>
          </Col>

          {/* Tổng tiền đang sở hữu + thu nhập + chi tiêu) */}
          <Col span={10}>
            <Card style={{ borderRadius: "12px", textAlign: "center" }}>
              <Title level={3} style={{ margin: 0 }}>
                Số dư
              </Title>
              <div style={{ marginTop: "12px" }}>
                <Text></Text>
                <Tag color="green">
                  <ArrowUpOutlined /> Thu
                </Tag>
                <Tag color="red">Chi</Tag>
              </div>
            </Card>
          </Col>
          
        </Row>
      </Content>
    </Layout>
  );
};

export default DashboardPage;
