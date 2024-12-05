import { Form, Input, Select, DatePicker, InputNumber, Button } from "antd";
import { Modal } from "antd/lib";
import React, { useMemo } from "react";

const { TextArea } = Input;
const { Option } = Select;

const AddNewTransactionModal = ({currentMaxId, setCurrentMaxId, isOpened, setOpenAddingModal}) => {
    const [form] = Form.useForm();
    
    const onFinish = (values) => {
        console.log('Form values:', values);
    };

    const onFinishFailed = (errorInfo) => {
        console.log('Failed:', errorInfo);
    };
    
    return (
        <>
        <Modal 
            className="modal-create-transaction bg-panel text-elements-primary"
            content={{style:{className:"bg-elements"}}}
            title="Add new transaction"
            open={isOpened} 
            maskClosable={false} 
            onCancel={() => setOpenAddingModal(false)}
            okButtonProps={{style: { htmlType:'submit'} }} 
            centered
            width="50%"
        >
            {isOpened && console.log("Open adding modal")}
            <Form
                form={form}
                layout="horizontal"
                labelCol={{span: 3,}}
                wrapperCol={{span: 20,}}
                onFinish={onFinish}
                onFinishFailed={onFinishFailed}
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
                    <Input placeholder="###" width="20px" 
                            disabled  
                            value={currentMaxId}/>
                </Form.Item>
                {/* Name */}
                <Form.Item
                    label="Name"
                    name="name"
                    rules={[{ required: true, message: 'Please input the name!' }]}
                >
                    <Input placeholder="Enter name" />
                </Form.Item>

                {/* Date */}
                <Form.Item
                    label="Date"
                    name="date"
                    rules={[{ required: true, message: 'Please select a date!' }]}
                >
                    <DatePicker style={{ width: '100%' }} />
                </Form.Item>

                {/* Type */}
                <Form.Item
                    label="Type"
                    name="type"
                    rules={[{ required: true, message: 'Please select a type!' }]}
                >
                    <Select placeholder="Select a type">
                        <Option value="thu">Thu</Option>
                        <Option value="chi">Chi</Option>
                    </Select>
                </Form.Item>

                {/* Total (Money) */}
                <Form.Item
                    label="Total"
                    name="total"
                    rules={[{ required: true, message: 'Please input the total!' }]}
                >
                    <InputNumber
                    style={{ width: '100%' }}
                    placeholder="Enter total amount"
                    formatter={(value) => `$ ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
                    parser={(value) => value.replace(/\$\s?|(,*)/g, '')}
                    />
                </Form.Item>

                {/* Note */}
                <Form.Item label="Note" name="note">
                    <TextArea rows={4} placeholder="Detail information !!" />
                </Form.Item>

                {/* Submit Button */}
                <Form.Item>
                    <Button type="primary" htmlType="submit" style={{ width: '100%' }}>
                    Submit
                    </Button>
                </Form.Item>

            </Form>
        </Modal>
        </>
    )
}

export default AddNewTransactionModal;