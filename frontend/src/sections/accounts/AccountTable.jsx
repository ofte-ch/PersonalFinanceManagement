import { ExportOutlined } from "@ant-design/icons";
import { Button, Input, Table, Tag } from "antd";
import { useState } from "react";
import useAccountColumn from "./AccountColumns";
import { useAccounts } from "../../api/accounts/get-accounts";

export const AccountTable = () => {
  const [page, setPage] = useState(1);
  const [keyword, setKeyword] = useState("");
  const [pageSize, setPageSize] = useState(5);
  const {data,isLoading} = useAccounts({page,size:pageSize,keyword});
  const columns = useAccountColumn(page, pageSize);
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
        scroll={{ x:600 }}
        loading={isLoading}
        title={() => (
          <div style={{ display: "flex", justifyContent: "space-between" }}>
            <Input.Search
              placeholder="Nhập tên tài khoản..."
              className="w-[250px]"
              allowClear
              onSearch={(value) => {
                setKeyword(value);
                setPage(1);
              }}
            />
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
