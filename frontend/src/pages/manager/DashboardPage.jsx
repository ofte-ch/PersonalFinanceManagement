import React, { useEffect, useState } from "react";
import {
  Layout,
  Card,
  Row,
  Col,
  Typography,
  Tag,
  InputNumber,
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
  // Tính số dư
  const [totalBalance, setTotalBalance] = useState(0);
  const [totalIncome, setTotalIncome] = useState(0);
  const [totalExpense, setTotalExpense] = useState(0);

  useEffect(() => {
    getAllAccounts().then(
      (accounts) => {
        setAccounts(accounts)
        const total = accounts.reduce((soTien, account) => soTien + (account.soDu || 0), 0);
        setTotalBalance(total);
        console.log(totalBalance);
      });
  }, []);

  // Lấy giao dịch
  const currentDate = moment().format("YYYY-MM-DD HH:mm:ss");
  const tenDaysBeforeDate = moment().subtract(30, "days").format("YYYY-MM-DD HH:mm:ss");

  const { data: transactions, isLoading } = useGetTransactionsByDateRange({
    page: 1,
    size: 10,
    TuNgay: tenDaysBeforeDate,
    DenNgay: currentDate,
  });
  // Tính tổng thu nhập và chi tiêu
  useEffect(() =>{
    if (transactions?.data) {
      const income = transactions.data
        .filter((item) => item.theLoai?.phanLoai === "Thu")
        .reduce((soTien, item) => soTien + item.tongTien, 0);
  
      const expense = transactions.data
        .filter((item) => item.theLoai?.phanLoai === "Chi")
        .reduce((soTien, item) => soTien + item.tongTien, 0);
  
      setTotalIncome(income);
      setTotalExpense(expense);
    }
  }, [transactions]);

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
              <Title level={5}>Giao dịch gần đây</Title>
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
              <div>
                <Title level={3} style={{ margin: 0 }}>{"Số dư (VND)"}</Title>
                <Text style={{ fontSize: "24px", fontWeight: "bold", color: "#000" }}>
                  {`${totalBalance.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',')}`}
                </Text>
              </div>

              <div style={{ marginTop: "12px" }}>
                <Text></Text>
                <Tag color="green" style={{ padding: "6px 12px", fontSize: "16px" }}>
                  <ArrowUpOutlined /> Thu
                  <Text style={{ fontSize: "20px", marginLeft: "8px", color: "#28a745" }}>
                    {`${totalIncome.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',')}`}
                  </Text>
                </Tag>
                <Tag color="red" style={{ padding: "6px 12px", fontSize: "16px" }}>
                  <CreditCardOutlined/> Chi
                  <Text style={{ fontSize: "20px", marginLeft: "8px", color: "#d32f2f" }}>
                    {`${totalExpense.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',')}`}
                  </Text>
                </Tag>
              </div>
            </Card>
          </Col>
          
        </Row>
      </Content>
    </Layout>
  );
};

export default DashboardPage;
