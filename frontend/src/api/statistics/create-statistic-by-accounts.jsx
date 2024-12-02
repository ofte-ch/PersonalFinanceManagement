import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "~/configs/api";

export const getStatisticByAccounts = ({ tuNgay, denNgay }) => {
    return api.post(`/thongke/byaccount`, {
        tuNgay,
        denNgay,
    });
}

export const useStatisticByAccounts = (options = {}) => {
    const { onSuccess, onError, ...restConfig } = options;

    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: getStatisticByAccounts,
        onSuccess: (data, ...args) => {
          queryClient.invalidateQueries({
            queryKey: ["accounts"],
          });
          onSuccess?.(data, ...args);
        },
        onError: (error, ...args) => {
          onError?.(error, ...args);
        },
        ...restConfig,
    });
}
