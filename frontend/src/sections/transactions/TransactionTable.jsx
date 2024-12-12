import { ExportOutlined } from "@ant-design/icons";
import { Button, Input, Popconfirm, Select, Space, Table, Tag } from "antd";
import { useState } from "react";
import useTransactionColumn from "./TransactionColumns";
import { useGetTransactions } from "~/api/transactions/get-transactions";
import { useAccounts } from "~/api/accounts/get-accounts";

const { Option } = Select;

export const TransactionTable = () => {
  const [page, setPage] = useState(1);
  const [keyword, setKeyword] = useState("");
  const [pageSize, setPageSize] = useState(5);
  const [maTaiKhoan, setMaTaiKhoan] = useState("");
  const {data,isLoading} = useGetTransactions({page,size:pageSize,keyword,maTaiKhoan});
  const columns = useTransactionColumn(page,pageSize);

  const {data: accounts, isLoadingAccounts} = useAccounts({page:1, size:100, keyword:""});
  const [selectedAccount, setSelectedAccount] = useState("Tất cả");

  return (
    <>
      <Table
        columns={columns}
        dataSource={data?.data || []}
        size="small"
        rowKey={(record) => record.id}
        pagination={{
          current: data?.currentPage,
          pageSize: data?.pageSize,
          total: data?.totalCount,
          showSizeChanger: true,
          pageSizeOptions: ["5","10", "20", "50", "100"],
          onShowSizeChange: (current, size) => {
            setPageSize(size);
            setPage(1);
          },
          onChange: (newPage) => setPage(newPage),
        }}
        loading={isLoading}
        scroll={{ x: "max-content" }}
        title={() => (
          <div style={{ display: "flex", justifyContent: "space-between" }}>
            <Input.Search
              placeholder="Nhập tên giao dịch ..."
              className="w-[250px]"
              allowClear
              onSearch={(value) => {
                setKeyword(value);
                setPage(1);
              }}
            />
            <Space align="center"
                style={{
                  fontSize: "12px", // Chỉnh cỡ chữ nhỏ hơn
                  display: "flex",
                  gap: "4px",
                }}>
              <label style={{ fontWeight: "bold" }}>Tài khoản: </label>
              <Select
                  placeholder="Chọn tài khoản"
                  value={selectedAccount}
                  onChange={(option) => {
                      setSelectedAccount(option);
                      setMaTaiKhoan(option === "Tất cả" ? "" : 
                        accounts?.data.find(tk => tk.id === option)?.id || "");
                      setPage(1);
                  }}
              >
                <Option key="0" value="Tất cả">Tất cả</Option>
                {accounts && accounts?.data.map((account) => (
                  <Option key={account.id} value={account.id}>
                    {account.tenTaiKhoan}
                  </Option>
                ))}
              </Select>
            </Space>
            */}
            {/*
            <Button icon={<ExportOutlined />}>
              Export <Tag color="blue">Coming Soon</Tag>
            </Button>
             */}
          </div>
        )}
      />
    </>
  );
};
