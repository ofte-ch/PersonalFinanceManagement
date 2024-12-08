
import { Input, Table } from "antd";
import { useState } from "react";
import useAccountTypeColumns from "./AccountTypeColumns";
import { useAccountTypes } from "../../api/account-types/get-account-types";

export const AccountTypeTable = () => {
  const columns = useAccountTypeColumns();
//   const [page, setPage] = useState(1);
//   const [keyword, setKeyword] = useState("");
//   const [pageSize, setPageSize] = useState(10);
// {page, size: pageSize, keyword}

/*
pagination={{
          current: data?.currentPage,
          pageSize: data?.pageSize,
          total: data?.totalCount,
          showSizeChanger: true,
          pageSizeOptions: ["10", "20", "50", "100"],
          onShowSizeChange: (current, size) => {
            setPageSize(size);
            setPage(1);
          },
          onChange: (newPage) => setPage(newPage),
        }}
*/

// onSearch={(value) => {
//     setKeyword(value);
//     setPage(1);
//   }}

  const { data: accountTypes } = useAccountTypes();
  return (
    <>
      <Table
        columns={columns}
        dataSource={accountTypes ?? []}
        size="small"
        rowKey={(record) => record.id}
        
        // loading={isLoading}
        title={() => (
          <div style={{ display: "flex", justifyContent: "space-between" }}>
            <Input.Search
              placeholder="Search account type..."
              className="w-[250px]"
              allowClear
              
            />
            
          </div>
        )}
      />
    </>
  );
};
