import {
  Button,
  Form,
  Input,
  Modal,
  Select,
  Row,
  Col,
  message,
  InputNumber,
} from "antd";
import { Flex } from "antd";
import { useCreateAccount } from "~/api/accounts/create-account";
import { useAccountStore } from "~/stores/accounts/accountStore";
import { useAccountTypes } from "~/api/account-types/get-account-types";
const CreateAccountModal = () => {
  const [form] = Form.useForm();
  const {data: accountTypes} = useAccountTypes({page:1, size:100,keyword:""});
  const { openCreateModal, setOpenCreateModal } = useAccountStore();

  const mutation = useCreateAccount({
    onSuccess: () => {
      form.resetFields();
      setOpenCreateModal(false);
      message.success("Tạo tài khoản mới thành công !");
    },
    onError: (error) => {
      message.error(`Tạo tài khoản mới thất bại !!!\n\nNguyên nhân: ${error.response.data}`);
    },
  });

  const onFinish = (values) => {
    mutation.mutate(values);
  };
  return (
    <Modal
      title={"Tạo tài khoản"}
      open={openCreateModal}
      onCancel={() => setOpenCreateModal(false)}
      maskClosable={false} 
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
              <InputNumber
                    placeholder="Nhập số dư..." 
                    style={{ width: '100%' }}
                    formatter={(value) => `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
                    parser={(value) => value.replace(/\$\s?|(,*)/g, '')}
                />
            </Form.Item>
          </Col>
        </Row>
        <Form.Item className="pt-4 m-0">
          <Flex justify="end" className="gap-3">
            <Button loading={false} type="default" htmlType="reset">
              Reset
            </Button>
            <Button loading={false} type="primary" htmlType="submit">
              Tạo
            </Button>
          </Flex>
        </Form.Item>
      </Form>
    </Modal>
  );
};
export default CreateAccountModal;
