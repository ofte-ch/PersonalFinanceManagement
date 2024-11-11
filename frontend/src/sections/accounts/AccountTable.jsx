import { ExportOutlined } from "@ant-design/icons";
import { Button, Input, Table, Tag } from "antd";
import { useState } from "react";
import useAccountColumn from "./AccountColumns";
import { useAccounts } from "../../api/accounts/get-accounts";

export const AccountTable = () => {
  const columns = useAccountColumn();
  const [page, setPage] = useState(1);
  const [keyword, setKeyword] = useState("");
  const {data:accounts,isLoading} = useAccounts({page,size:10,keyword});
  return (
    <>
      <Table
        columns={columns}
        dataSource={accounts}
        size="middle"
        rowKey={(account) => account.id}
        pagination={{
          current: page,
          pageSize: 10,
          total: accounts?.length || 0,
          onChange: (newPage) => setPage(newPage),
        }}
        loading={isLoading}
        title={() => (
          <div style={{ display: "flex", justifyContent: "space-between" }}>
            <Input.Search
              placeholder="Search accounts..."
              className="w-[250px]"
              allowClear
              onSearch={(value) => {
                setKeyword(value);
                setPage(1);
              }}
            />
            <Button icon={<ExportOutlined />}>
              Export <Tag color="blue">Coming Soon</Tag>
            </Button>
          </div>
        )}
      />
    </>
  );
};
