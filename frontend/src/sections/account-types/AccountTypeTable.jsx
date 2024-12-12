
import { Button, Input, Table, Tag } from "antd";
import { useState } from "react";
import useAccountTypeColumns from "./AccountTypeColumns";
import { ExportOutlined } from "@ant-design/icons";
import { useAccountTypes } from "../../api/account-types/get-account-types";

export const AccountTypeTable = () => {
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [keyword, setKeyword] = useState("");
  const { data: accountTypes, isLoading } = useAccountTypes({page, size:pageSize, keyword});
  const columns = useAccountTypeColumns(page, pageSize);

  return (
    <>
      <Table
        columns={columns}
        dataSource={accountTypes ?? []}
        size="small"
        rowKey={(record) => record.id}
        scroll={{ x:600 }}
        loading={isLoading}
        pagination={{
          current: accountTypes?.currentPage,
          pageSize: accountTypes?.pageSize,
          total: accountTypes?.totalCount,
          showSizeChanger: true,
          pageSizeOptions: ["10", "20", "50", "100"],
          onShowSizeChange: (current, size) => {
            setPageSize(size);
            setPage(1);
          },
          onChange: (newPage) => setPage(newPage),
        }}
        title={() => (
          <div style={{ display: "flex", justifyContent: "space-between" }}>
            <Input.Search
              placeholder="Nhập tên loại tài khoản..."
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
