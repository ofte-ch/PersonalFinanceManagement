import { create } from "zustand";

export const useTransactionStore = create((set) => ({
    transaction: null,
    accountList: null,
    openCreateModal: false,
    openUpdateModal: false,
    openDeleteModal: false,
    setTransaction: (transaction) => set({ transaction }),
    setAccountList: (accountList) => set({ accountList }),
    setOpenCreateModal: (openCreateModal) => set({ openCreateModal }),
    setOpenUpdateModal: (openUpdateModal) => set({ openUpdateModal }),
    setOpenDeleteModal: (openDeleteModal) => set({ openDeleteModal }),
}));
