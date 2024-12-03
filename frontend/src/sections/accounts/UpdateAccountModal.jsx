import { Button, Form, Input, Modal, Row, Col, message,Select } from "antd";
import { Flex } from "antd";
import { useUpdateAccount } from "~/api/accounts/update-account";
import { useAccountStore } from "~/stores/accounts/accountStore";
import { useEffect } from "react";
import { useAccountTypes } from "~/api/account-types/get-account-types";
const UpdateAccountModal = () => {
  const [form] = Form.useForm();
  const {data: accountTypes} = useAccountTypes();
  const { openUpdateModal, setOpenUpdateModal, account } = useAccountStore();

  const mutation = useUpdateAccount({
    onSuccess: () => {
      form.resetFields();
      message.success("Account updated successfully");
    },
    onFinish: () => {
      message.error("Failed to update account !");
    },
  });

  const onFinish = (values) => {
    mutation.mutate({
      id: account.id,
      data: values,
    });
    setOpenUpdateModal(false);
  };

  useEffect(() => {
    if (account) {
      form.setFieldsValue({
        ...account,
      });
    }
  }, [account, form]);

  return (
    <Modal
      title={"Cập nhật tài khoản"}
      open={openUpdateModal}
      onCancel={() => setOpenUpdateModal(false)}
      footer={null}
    >
      <Form
        form={form}
        onFinish={onFinish}
        className="pt-4"
        layout="vertical"
        variant="filled"
      >
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Tên tài khoản"
              name="tenTaiKhoan"
              rules={[{ required: true, message: "Nhập tên tài khoản" }]}
            >
              <Input placeholder="Nhập tên tài khoản..." />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              label="Loại tài khoản"
              name="loaiTaiKhoanId"
              rules={[{ required: true, message: "Chọn loại tài khoản" }]}
            >
              <Select placeholder="Chọn loại tài khoản">
                {accountTypes?.map((accountType) => (<Option key = {accountType.id} value = {accountType.id}>{accountType.ten}</Option>))}
              </Select>
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Số dư"
              name="soDu"
              rules={[{ required: true, message: "Nhập số dư" }]}
            >
              <Input placeholder="Nhập số dư..." />
            </Form.Item>
          </Col>
        </Row>
        <Form.Item className="pt-4 m-0">
          <Flex justify="end" className="gap-3">
            <Button loading={false} type="default" htmlType="reset">
              Reset
            </Button>
            <Button loading={false} type="primary" htmlType="submit">
              Submit
            </Button>
          </Flex>
        </Form.Item>
      </Form>
    </Modal>
  );
};
export default UpdateAccountModal;
