import React, { useEffect, useState } from "react";
import { Layout, Card, Row, Col, Typography, Tag, Divider, List, Progress } from "antd";
import { CreditCardOutlined, ArrowUpOutlined } from "@ant-design/icons";
import PageHeader from "~/components/page-header";
import { getAllAccounts } from "~/api/accounts/get-accounts";
import moment from "moment";
import { useGetTransactionsByDateRange } from "~/api/transactions/get-transaction-for-dashboard";

const { Header, Content } = Layout;
const { Title, Text } = Typography;

const DashboardPage = () => {
  // Lấy tk
  const [accounts, setAccounts] = useState([]);
  useEffect(() => {
    getAllAccounts().then((accounts) => setAccounts(accounts));
  }, []);
  // Lấy giao dịch
  const {data: transactions, isLoading} = useGetTransactionsByDateRange({page:1, size:10, TuNgay:"2024-11-11 00:00:00", DenNgay:"2024-12-11 02:23:26"});
  return (
    <Layout style={{ minHeight: "100vh", background: "#f5f5f5" }}>
      {/* Header */}
      <PageHeader
          heading={("Dashboard")}
          links={[
            { title: ("Dashboard"), href: "/" },
          ]}
        />
      {/* Content */}
      <Content>

      {/*Dòng đầu gồm : 
            Biểu đồ giao dịch trong tháng vừa qua 
            Số dư của user
            Danh sách các tài khoản
      */}
      <Row gutter={[8, 8, 8]} style={{ marginTop: "24px" }}>
          {/* Monthly Earnings */}
          <Col span={10}>
            <Card style={{ borderRadius: "12px" }} >
              <Title level={5}>Monthly</Title>

              {/* Biểu đồ */}
              <div style={{ textAlign: "center" }}>
                <div style={{ height: "100px", background: "#f0f0f0", marginTop: "12px" }}>
                  **** Biểu đồ ở đây ****
                </div>
              </div>
            
            </Card>
          </Col>
          {/* Tổng tiền đang sở hữu + thu nhập + chi tiêu) */}
          <Col span={8}>
            <Card style={{ borderRadius: "12px", textAlign: "center" }}>
              <Title level={3} style={{ margin: 0 }}>
                Balance
              </Title>
              <div style={{ marginTop: "12px" }}>
                <Text></Text>
                <Tag color="green">
                  <ArrowUpOutlined /> Income
                </Tag>
                <Tag color="red">Expenses</Tag>
              </div>
            </Card>
          </Col>
          {/* Danh sách các tài khoản */}
          <Col span={6}>
            <Card style={{ borderRadius: "12px", }}>
              <Title level={5}>Accounts</Title>
              <Row style={{ backgroundColor: "#f0f0f0", padding: "8px", fontWeight: "bold", textAlign: "center" }}>
                <Col span={12}>Name</Col>
                <Col span={12}>Total</Col>
              </Row>
              <List
                dataSource={accounts}
                renderItem={(item) => (
                  <List.Item>
                    <Row>
                      <Col span={12}>{item.tenTaiKhoan}</Col>
                      <Col style={{textAlign:"center"}} span={12}>{`${item.soDu.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',')} VND`}</Col>
                      </Row>
                  </List.Item>
                )}
              />
            </Card>
          </Col>
        </Row>
        {/*Dòng 2 bao gồm:
            + Danh sách giao dịch
            + ...
        */}
        <Row gutter={[10, 10]}>

          {/* Danh sách các giao dịch */}
          <Col span={12}>
            <Card style={{ borderRadius: "12px" }}>
              <Title level={5}>Transactions</Title>
              <Row 
                style={{ backgroundColor: "#f0f0f0", padding: "8px", fontWeight: "bold", textAlign: "center" }}>
                <Col span={8}>Name</Col>
                <Col span={8}>Date</Col>
                <Col span={8}>Total</Col>
              </Row>
              <List
                dataSource={transactions?.data}
                renderItem={(item) => (
                  <List.Item>
                    <Row>
                      <Col style={{borderRight: "1px solid #d9d9d9"}} span={8}>{item.tenGiaoDich}</Col>
                      <Col style={{textAlign:"center", borderRight: "1px solid #d9d9d9"}} span={8}>{moment(item.ngayGiaoDich).format("YYYY-MM-DD")}</Col>
                      <Col style={{textAlign:"center"}} span={8}>{`${item.tongTien.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',')} VND`}</Col>
                    </Row>
                  </List.Item>
                )}
              />
            </Card>
          </Col>

          {/* ... */}
          <Col span={12}>
            <Card style={{ borderRadius: "12px" }}>
              <Title level={5}>Payable Accounts</Title>
              <Text>Keep your accounts up to date to avoid issues.</Text>
              <Divider />
              <Progress percent={87.5} strokeColor="#1677ff" />
            </Card>
          </Col>
        </Row>

        
      </Content>
    </Layout>
  );
};

export default DashboardPage;