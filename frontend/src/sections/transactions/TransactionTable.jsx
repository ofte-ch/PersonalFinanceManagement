import { ExportOutlined } from "@ant-design/icons";
import { Button, Input, Popconfirm, Select, Space, Table, Tag } from "antd";
import { useEffect, useState } from "react";
import useTransactionColumn from "./TransactionColumns";
import { useGetTransactions } from "~/api/transactions/get-transactions";
import { getAllAccounts } from "~/api/accounts/get-accounts";

const { Option } = Select;

export const TransactionTable = () => {
  const [page, setPage] = useState(1);
  const [keyword, setKeyword] = useState("");
  const [pageSize, setPageSize] = useState(5);
  const [maTaiKhoan, setMaTaiKhoan] = useState("");
  const {data,isLoading} = useGetTransactions({page,size:pageSize,keyword,maTaiKhoan});
  const columns = useTransactionColumn(page,pageSize);
/*
  const [accounts, setAccounts] = useState([]);
  const [selectedAccount, setSelectedAccount] = useState("All");
  useEffect(() => {
    getAllAccounts().then((accounts) => setAccounts(accounts));
  }, []);
*/
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
        scroll={{ x:600 }}
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
            {/*
            <Space align="center">
              <label >Account: </label>
              <Select
                  placeholder="Choose account"
                  value={selectedAccount}
                  onChange={(option) => {
                      setSelectedAccount(option);
                      setMaTaiKhoan(option === "All" ? "" : 
                        accounts.find(tk => tk.id === option)?.id || "");
                      setPage(1);
                  }}
              >
                <Option key="0" value="All">All</Option>
                {accounts && accounts.map((account) => (
                  <Option key={account.id} value={account.id}>
                    {account.tenTaiKhoan}
                  </Option>
                ))}
              </Select>
            </Space>
            */}
            <Popconfirm
              title="Xuất file"
              description="Chức năng này sẽ sớm ra mắt !!!">
                <Button icon={<ExportOutlined />}>
                  Export
                </Button>
            </Popconfirm>
          </div>
        )}
      />
    </>
  );
};
