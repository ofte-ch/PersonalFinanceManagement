import { Form, Input, Select, DatePicker, InputNumber, 
        Button, Typography, Space } from "antd";
import { Modal } from "antd/lib";
import { EditOutlined, EditFilled } from "@ant-design/icons";
import moment from "moment";
import React, { useEffect, useState } from "react";
import { useUpdateTransaction } from "~/api/transactions/update-transaction";

const { TextArea } = Input;
const { Option } = Select;
 
const UpdateTransactionModal = (
    {accountList,transaction, setSelectedTransaction, isOpened, setOpenUpdateModal}
) => {
    // Form instance
    const [form] = Form.useForm();
    // Title
    const [title, setTitle] = useState("");
    // Check if you are editing
    const [isEditing, setIsEditing] = useState(false);
    // Mutation
    const mutation = useUpdateTransaction();
    // Set transaction whenever data is loaded
    useEffect( () => {
        if(transaction){
            form.setFieldsValue({
                ...transaction,
                ngayGiaoDich:moment(transaction.ngayGiaoDich)
            });
            setTitle(`Transaction ${transaction.id}`);
        }
    }, [transaction, form]);

    const handleUpdate = (values) => {
        console.log(values);
        mutation.mutate({
            id: transaction.id,
            data: values,
          });
    }
    // Toggle edit mode
    const handleEditToggle = () => {
        setIsEditing((prev) => !prev); 
    };

    function handleCloseModal() {
        setOpenUpdateModal(false);
        setSelectedTransaction(null);
    }

    return (
        <Space direction="vertical" sixze="medium" >
            <Modal 
                className="bg-panel text-elements-primary"
                content={{style:{className:"bg-elements"}}}
                open={isOpened} 
                title={title}
                maskClosable={false} 
                onCancel={() => handleCloseModal()}
                okButtonProps={{style: { display:"none" } }}
                width="50%"
                centered
            >
                {/* Edit button */}
                <Button 
                    icon={<EditOutlined/>}
                    onClick={() => handleEditToggle()}
                >
                    <strong>
                        {isEditing ? "Cancel Edit": "Edit"}
                    </strong>
                </Button>

                {/* Form */}        
                <Form
                    form={form}
                    title={`Transaction #`}
                    layout="horizontal"
                    labelCol={{span: 7,}}
                    wrapperCol={{span: 30,}}
                    onFinish={handleUpdate}
                    labelAlign="left"
                    autoComplete="off"
                >
                    {/* ID */}
                    <Form.Item
                        label="ID"
                        name="id"
                        tooltip="This field is locked and cannot be edited."                    
                        rules={[{ required: true, message: 'Please input the ID!' }]}
                    >
                        <Input width="20px" disabled/>
                    </Form.Item>
                    {/* ID */}
                    <Form.Item
                        label="Account"
                        name="taiKhoan"
                        rules={[{ required: true, message: 'Please choose account!' }]}
                    >
                        <Input width="20px" disabled/>
                    </Form.Item>
                    {/* Name */}
                    <Form.Item
                        label="Name"
                        name="tenGiaoDich"
                        rules={[{ required: true, message: 'Please input the name!' }]}
                    >
                        <Input placeholder="Enter name" disabled={!isEditing}/>
                    </Form.Item>

                    {/* Date */}
                    <Form.Item
                        label="Date"
                        name="ngayGiaoDich"
                        rules={[{ required: true, message: 'Please select a date!' }]}
                    >
                        <DatePicker style={{ width: '100%' }} disabled={!isEditing} />
                    </Form.Item>

                    {/* Type */}
                    <Form.Item
                        label="Type"
                        name="loaiGiaoDich"
                        rules={[{ required: true, message: 'Please select a type!' }]}
                    >
                        <Select placeholder="Select transaction type" disabled={!isEditing}>
                            <Option value="chi">Chi</Option>
                            <Option value="thu" >Thu</Option>
                        </Select>
                    </Form.Item>

                    {/* Total (Money) */}
                    <Form.Item
                        label="Total"
                        name="tongTien"
                        rules={[{ required: true, message: 'Please input the total!' }]}
                    >
                        <InputNumber
                        style={{ width: '100%' }}
                        placeholder="Enter total amount"
                        disabled={!isEditing}
                        formatter={(value) => `VND ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
                        parser={(value) => value.replace(/\$\s?|(,*)/g, '')}
                        />
                    </Form.Item>

                    {/* Note */}
                    <Form.Item label="Note" name="ghiChu">
                        <TextArea rows={4} disabled={!isEditing} placeholder="Detail information !!"/>
                    </Form.Item>
                    {/* Submit Button */}
                    <Form.Item>
                    {isEditing && (
                        <Button 
                            htmlType="submit"
                            style={{ marginLeft: 8 }} 
                        >
                            Save
                        </Button>
                    )}
                    </Form.Item>
                </Form>
            </Modal>
        </Space>
    )
}

export default UpdateTransactionModal;