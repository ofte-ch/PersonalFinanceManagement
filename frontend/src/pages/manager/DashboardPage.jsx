import React, { useEffect, useState } from "react";
import { Layout, Card, Row, Col, Typography, Tag, Divider, List, Progress } from "antd";
import { CreditCardOutlined, ArrowUpOutlined } from "@ant-design/icons";
import PageHeader from "~/components/page-header";
import { getAllAccounts } from "~/api/accounts/get-accounts";
import { useGetTransactions } from "~/api/transactions/get-transactions";
import moment from "moment";

const { Header, Content } = Layout;
const { Title, Text } = Typography;

const DashboardPage = () => {
  const [accounts, setAccounts] = useState([]);
  useEffect(() => {
    getAllAccounts().then((accounts) => setAccounts(accounts));
  }, []);
  const {data: transactions, isLoading} = useGetTransactions({page:1, size:10, keyword:"", maTaiKhoan:""});
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
            Danh sách các giao dịch trong tháng đó
      */}
      <Row gutter={[8, 8, 8]} style={{ marginTop: "24px" }}>
          {/* Monthly Earnings */}
          <Col span={10}>
            <Card style={{ borderRadius: "12px" }}>
              <Title level={5}>Monthly</Title>

              {/* Biểu đồ */}
              <div style={{ textAlign: "center" }}>
                <div style={{ height: "100px", background: "#f0f0f0", marginTop: "12px" }}>
                  **** Biểu đồ ở đây ****
                </div>
              </div>
            
            </Card>
          </Col>

          {/* Danh sách các giao dịch */}
          <Col span={8}>
            <Card style={{ borderRadius: "12px" }}>
              <Title level={5}>Transactions</Title>
              <List
                dataSource={transactions?.data}
                renderItem={(item) => (
                  <List.Item
                    style={{
                      display: "flex",
                      justifyContent: "space-between", // Căn đều các phần tử
                      alignItems: "center", // Căn giữa theo chiều dọc
                    }}
                  >
                    <Text style={{ flex: 1 }}>{item.tenGiaoDich}</Text>
                    <Text style={{ flex: 1 }}>{moment(item.ngayGiaoDich).format("YYYY-MM-DD")}</Text>
                    <Text style={{ flex: 1 }} strong>{`${item.tongTien.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',')} VND`}</Text>
                  </List.Item>
                )}
              />
            </Card>
          </Col>
          {/* Danh sách các tài khoản */}
          <Col span={6}>
            <Card style={{ borderRadius: "12px" }}>
              <Title level={5}>Accounts</Title>
              <List
                dataSource={accounts}
                renderItem={(item) => (
                  <List.Item>
                    <Text>{item.tenTaiKhoan}</Text>
                    <Text strong>{item.soDu}</Text>
                  </List.Item>
                )}
              />
            </Card>
          </Col>

        </Row>
        <Row gutter={[5, 5]}>
          {/* Tổng tiền đang sở hữu + thu nhập + chi tiêu) */}
          <Col span={12}>
            <Card style={{ borderRadius: "12px", textAlign: "center" }}>
              <Title level={3} style={{ margin: 0 }}>
                Balance
              </Title>
              <div style={{ marginTop: "12px" }}>
                <Tag color="green">
                  <ArrowUpOutlined /> Income
                </Tag>
                <Tag color="red">Expenses</Tag>
              </div>
            </Card>
          </Col>

          {/* Danh sách các tài khoản */}
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