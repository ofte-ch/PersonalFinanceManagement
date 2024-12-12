import { Button, Form, Input, Modal, Row, Col, message,Select, Spin, InputNumber } from "antd";
import { Flex } from "antd";
import { useUpdateAccount } from "~/api/accounts/update-account";
import { useAccountStore } from "~/stores/accounts/accountStore";
import { useEffect, useState } from "react";
import { useAccountTypes } from "~/api/account-types/get-account-types";
const UpdateAccountModal = () => {
  const [form] = Form.useForm();
  const {data: accountTypes} = useAccountTypes({page:1, size:100, keyword:""});
  const { openUpdateModal, setOpenUpdateModal, setAccount, account } = useAccountStore();
  const [isEditing, setIsEditing] = useState(false);
  const [isDirty, setIsDirty] = useState(false);

  const mutation = useUpdateAccount({
    onSuccess: () => {
      form.resetFields();
      message.success("Cập nhật tài khoản thành công !");
    },
    onError: (error) => {
      message.error(`Cập nhật tài khoản mới thất bại !!!\n\nNguyên nhân: ${error.response.data}`);
    },
  });

  const onFinish = (values) => {
    mutation.mutate({
      id: account.id,
      data: values,
    });
    setAccount(null);
    setOpenUpdateModal(false);
  };

  const handleCancel = () => {
    setAccount(null);
    setIsEditing(false);
    form.resetFields();
    setOpenUpdateModal(false);
  }

  const handleCancelWhileEditing = () => {
    if (isDirty) {
      Modal.confirm({
        title: "Bạn có chắc muốn hủy chỉnh sửa ?",
        content: "Những thay đổi chưa được lưu sẽ bị mất.",
        okText: "Hủy chỉnh sửa",
        cancelText: "Tiếp tục chỉnh sửa",
        onOk: () => {
          form.resetFields();
          setIsEditing(false);
          setIsDirty(false);
        },
      });
    } else {
      form.resetFields();
      setIsEditing(false);
    }
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
      onCancel={handleCancel}
      maskClosable={false}
      footer={null}
    >
      <Form
        form={form}
        onFinish={onFinish}
        className="pt-4"
        layout="vertical"
        variant="filled"
        initialValues={{
          tenTaiKhoan: account?.tenTaiKhoan,
          loaiTaiKhoanId: accountTypes?.id,
          soDu: account?.soDu,
        }}
        onValuesChange={() => setIsDirty(true)}
      >
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Tên tài khoản"
              name="tenTaiKhoan"
              rules={[{ required: true, message: "Nhập tên tài khoản" }]}
            >
              <Input placeholder="Nhập tên tài khoản..." disabled={!isEditing}/>
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              label="Loại tài khoản"
              name="loaiTaiKhoanId"
              rules={[{ required: true, message: "Chọn loại tài khoản" }]}
            >
              <Select placeholder="Chọn loại tài khoản" disabled={!isEditing}>
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
                  min={0}
                  disabled={!isEditing}
              />
            </Form.Item>
          </Col>
        </Row>
        <div className="flex justify-center space-x-5">
        {isEditing ? (
          <>
            <Button onClick={handleCancelWhileEditing}>Hủy</Button>
            <Button
              type="primary"
              htmlType="submit"
            >
              {mutation.isPending && (
                <Spin size="small" style={{ marginRight: 8 }} />
              )}
              Lưu
            </Button>
          </>
        ) : (
          <Button type="default" onClick={() => setIsEditing(true)}>
            Chỉnh sửa
          </Button>
        )}
        </div>
      </Form>
    </Modal>
  );
};
export default UpdateAccountModal;
