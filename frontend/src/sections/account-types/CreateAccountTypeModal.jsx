import {
    Button,
    Form,
    Input,
    Modal,
    Row,
    Col,
    message,
  } from "antd";

  import { Flex } from "antd";
  import { useCreateAccountType } from "~/api/account-types/create-account-type";
  import { useAccountTypeStore } from "~/stores/account-types/accountTypeStore";


  const CreateAccountTypeModal = () => {
    const [form] = Form.useForm();
    const { openCreateModal, setOpenCreateModal } = useAccountTypeStore();
  
    const mutation = useCreateAccountType({
      onSuccess: () => {
        form.resetFields();
        setOpenCreateModal(false);
        message.success("Tạo mới loại tài khoản thành công !");
      },
      onFinish: () => {
        message.error("Tạo mới loại tài khoản thất bại !!!");
      },
    });
  
    const onFinish = (values) => {
      mutation.mutate(values);
    };
    return (
      <Modal
        title={"Tạo loại tài khoản"}
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
            <Col span={24}>
              <Form.Item
                label="Tên loại tài khoản"
                name="Ten"
                rules={[{ required: true, message: "Nhập tên loại tài khoản" }]}
              >
                <Input placeholder="Nhập tên loại tài khoản..." />
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
  export default CreateAccountTypeModal;
  