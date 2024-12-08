import { Form, Input, Select, DatePicker, InputNumber, Button } from "antd";
import { Modal } from "antd/lib";
import moment from "moment";
import React, { useEffect, useMemo, useState } from "react";
import { useTransactionStore } from "~/stores/transactions/transactionStore";

const { TextArea } = Input;
const { Option } = Select;

const AddNewTransactionModal = ({accountList}) => {
    const [form] = Form.useForm();
    const { openCreateModal, setOpenCreateModal } = useTransactionStore();

    const onFinish = (values) => {
        console.log('Form values:', values);
    };

    const onFinishFailed = (errorInfo) => {
        console.log('Failed:', errorInfo);
    };

    const fillBalance = (value) => {
        const taikhoan = accountList.find((acc) => acc.id === value);
        if(taikhoan) {
            form.setFieldsValue({soDu: taikhoan.soDu || 0});
        }
    }
    
    return (
        <>
        <Modal 
            className="modal-create-transaction bg-panel text-elements-primary"
            content={{style:{className:"bg-elements"}}}
            title="Add new transaction"
            open={openCreateModal} 
            maskClosable={false} 
            onCancel={() => setOpenCreateModal(false)}
            okButtonProps={{style: { htmlType:'submit'} }} 
            centered
            width="55%"
        >
            <Form
                form={form}
                layout="vertical"
                onFinish={onFinish}
                onFinishFailed={onFinishFailed}
                labelAlign="left"
                autoComplete="off"
                initialValues={{
                    ngayGiaoDich: moment().startOf('day'), // Hoặc dùng dayjs().startOf('day') nếu dùng Day.js
                }}>
                <div style={{ display: "flex", gap: "20px" }}>
                    <Form.Item
                        label="ID"
                        name="id"
                        style={{ flex: 1 }}
                    >
                        <Input style={{ width: '100%' }} disabled/>
                    </Form.Item>
                    <Form.Item
                        label="Ngày tạo"
                        name="ngayGiaoDich"
                        style={{ flex: 1 }}
                    >
                        <DatePicker style={{ width: '100%' }} disabled/>
                    </Form.Item>
                    <Form.Item
                        label="Loại"
                        name="type"
                        style={{ flex: 1 }}
                    >
                        <Select>
                            <Option key="thu" name="thu" value="Thu"></Option>
                            <Option key="chi" name="chi" value="Chi"></Option>
                        </Select>
                    </Form.Item>
                </div>
                <Form.Item
                    label="Tên giao dịch"
                    name="tenGiaoDich"
                    rules={[{ required: true, message: 'Vui lòng nhập tên giao dịch !!!'}]}
                >
                    <Input placeholder="Vui lòng nhập tên giao dịch ..." />
                </Form.Item>
                {/* Tài khoản gửi và tài khoản nhận */}
                <div style={{ display: "flex", gap: "20px" }}>
                    <Form.Item
                        label="Tài khoản chuyển"
                        name="taiKhoanChuyen"
                        rules={[{ required: true, message: 'Vui lòng chọn tài khoản chuyển !!!' }]}
                        style={{ flex: 1 }}
                    >
                        <Select onChange={fillBalance}>
                            {(accountList) && accountList.map((account) => (
                            <Option key={account.id} value={account.id}>
                                {account.tenTaiKhoan}
                            </Option>
                            ))}
                        </Select>
                    </Form.Item>
                    <Form.Item
                        label="Tài khoản nhận"
                        name="taiKhoanNhan"
                        rules={[{ required: true, message: 'Vui lòng chọn tài khoản nhận !!!' }]}
                        style={{ flex: 1 }}
                    >
                        <Select onChange={fillBalance}>
                            {(accountList) && accountList.map((account) => (
                            <Option key={account.id} value={account.id}>
                                {account.tenTaiKhoan}
                            </Option>
                            ))}
                        </Select>
                    </Form.Item>
                </div>
                {/* Tổng tiền và số dư */}
                <div style={{ display: "flex", gap: "20px" }}>
                    <Form.Item
                        label="Tổng tiền"
                        name="tongTien"
                        rules={[{ required: true, message: 'Vui lòng nhập tổng tiền !!!' }]}
                        style={{ flex: 1 }}
                    >
                        <InputNumber
                            style={{ width: '100%' }}
                            formatter={(value) => `$ ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
                            parser={(value) => value.replace(/\$\s?|(,*)/g, '')}
                        />
                    </Form.Item>
                    <Form.Item
                        label="Số dư"
                        name="soDu"
                        style={{ flex: 1 }}
                    >
                        <InputNumber
                            style={{ width: '100%' }}
                            prePlaceholder="0"
                            formatter={(value) => `$ ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
                            parser={(value) => value.replace(/\$\s?|(,*)/g, '')}
                            disabled
                        />
                    </Form.Item>
                </div>

                {/* Ghi chú riêng 1 hàng */}
                <Form.Item label="Ghi chú" name="ghiChu">
                    <TextArea rows={4} placeholder="Ghi chú giao dịch ..." />
                </Form.Item>
            </Form>
        </Modal>
        </>
    )
}

export default AddNewTransactionModal;