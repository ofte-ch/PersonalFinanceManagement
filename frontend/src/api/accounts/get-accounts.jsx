import { queryOptions, useQuery } from "@tanstack/react-query";
import { api } from "~/configs/api";

export const getAccounts = async ({ page = "", size = "", keyword = "" }) => {
  const response = await api.get(`/accounts`, {
    params: {
      page,
      size,
      keyword,
    },
  });
  return response;
};

export const getAccountsQueryOptions = ({ page, size, keyword }) => {
  return queryOptions({
    queryKey: page ? ["accounts", { page, size, keyword }] : ["accounts"],
    queryFn: () => getAccounts({ page, size, keyword }),
  });
};

export const useAccounts = ({ queryConfig, page, size, keyword }) => {
  return useQuery({
    ...getAccountsQueryOptions({ page, size, keyword }),
    ...queryConfig,
  });
};

export const getAllAccounts = async () => {
  const response = await api.get(`/accounts`);
  return response.data;
};
