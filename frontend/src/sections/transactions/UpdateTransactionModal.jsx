import {
  Flex,
  Button,
  Form,
  Input,
  InputNumber,
  Modal,
  Row,
  Col,
  message,
  Select,
  DatePicker,
} from "antd";
import { EditOutlined, EditFilled } from "@ant-design/icons";
import { useUpdateTransaction } from "~/api/transactions/update-transaction";
import { getAllAccounts } from "~/api/accounts/get-accounts";
import { useTransactionStore } from "~/stores/transactions/transactionStore";
import { useEffect, useState } from "react";
import { useTypes } from "~/api/types/get-types";
import moment from "moment";

const { TextArea } = Input;
const UpdateTransactionModal = () => {
  const [form] = Form.useForm();
  const { openUpdateModal, setOpenUpdateModal, setTransaction, transaction } = useTransactionStore();
  const [accounts, setAccounts] = useState([]);
  useEffect(() => {
    getAllAccounts().then((accounts) => setAccounts(accounts));
  }, []);
  
  const { data: types } = useTypes();
  const specialType = types?.find(x => x.tenTheLoai === "Giao dịch giữa 2 tài khoản").id;
  
  const [isTwoAccTx, setIsTwoAccTx] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [isDirty, setIsDirty] = useState(false);

  // ================================================================= Xử lý disabled tk phụ =================================================================
  const checkIfInterAccTranfer = (typeOption) => {
    if(typeOption === specialType)
      setIsTwoAccTx(true);
    else{
      form.setFieldValue("taiKhoanPhu", null);
      setIsTwoAccTx(false);
    }
  }
  useEffect(() => {
    if (isEditing) {
      const currentType = form.getFieldValue("theLoai");
      checkIfInterAccTranfer(currentType); // Kiểm tra loại giao dịch hiện tại
    }
  }, [isEditing, form]);

  // ================================================================= Xử lý hủy edit =================================================================
  const handleCancel = () => {
    setTransaction(null);
    setIsEditing(false);
    setIsTwoAccTx(false);
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
          setIsTwoAccTx(false);
          form.resetFields();
          setIsEditing(false);
          setIsDirty(false);
        },
      });
    } else {
      setIsTwoAccTx(false);
      form.resetFields();
      setIsEditing(false);
    }
  };

  const mutation = useUpdateTransaction({
    onSuccess: () => {
      form.resetFields();
      message.success("Cập nhật giao dịch thành công !");
    },
    onFinish: () => {
      message.error("Cập nhật giao dịch thất bại !!!");
    },
  });

  const onFinish = (values) => {
    const { taiKhoanGoc, taiKhoanPhu, theLoai, ...rest } = values;
    const formatedValues = {
      ...rest,
      tenGiaoDich: values.tenGiaoDich.trim(),
      ngayGiaoDich: moment(values.ngayGiaoDich).format("YYYY-MM-DD HH:mm:ss"),
      taiKhoanGocId: taiKhoanGoc,
      taiKhoanPhuId: taiKhoanPhu,
      theLoaiId: theLoai,
    }
    mutation.mutate({
      id: transaction.id,
      data: formatedValues,
    });
    setTransaction(null);
    setOpenUpdateModal(false);
    setIsEditing(false);
    setIsTwoAccTx(false);
  };

  useEffect(() => {
    if (transaction) {
      form.setFieldsValue({
        ...transaction,
        ngayGiaoDich: moment(transaction.ngayGiaoDich, "YYYY-MM-DD HH:mm:ss"),
        taiKhoanGoc: transaction.taiKhoanGoc ? transaction.taiKhoanGoc.id : null,
        taiKhoanPhu: transaction.taiKhoanPhu ? transaction.taiKhoanPhu.id : null,
        theLoai: transaction.theLoai ? transaction.theLoai.id : [],
      });
    }
  }, [transaction, form]);

  return (
    <Modal
      title={"Cập nhật giao dịch"}
      open={openUpdateModal}
      onCancel={handleCancel}
      footer={null}
      maskClosable={false}
    >
      <Form
        form={form}
        onFinish={onFinish}
        className="pt-4"
        layout="vertical"
        variant="filled"
        initialValues={{
          tenGiaoDich: transaction?.tenGiaoDich,
          ngayGiaoDich: moment(transaction?.ngayGiaoDich),
          taiKhoanGoc: transaction?.taiKhoanGoc ? transaction.taiKhoanGoc.id : null,
          taiKhoanPhu: transaction?.taiKhoanPhu ? transaction.taiKhoanPhu.id : null,
          theLoai: transaction?.theLoai ? transaction.theLoai.id : [],
          tongTien: transaction?.tongTien,
          ghiChu: transaction?.ghiChu,
        }}
        onValuesChange={() => setIsDirty(true)}
      >
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Tên giao dịch"
              name="tenGiaoDich"
              rules={[{ required: true, message: "Nhập tên giao dịch" }]}
            >
              <Input placeholder="Nhập tên giao dịch..." disabled={!isEditing}/>
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              label="Ngày giao dịch"
              name="ngayGiaoDich"
              rules={[{ required: true, message: "Chọn ngày giao dịch" }]}
            >
              <DatePicker style={{ width: "100%" }} disabled/>
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Tài khoản gốc"
              name="taiKhoanGoc"
              dependencies={["taiKhoanGoc"]}
              rules={[{ required: true, message: "Chọn tài khoản gốc !" }]}
            >
              <Select placeholder="Chọn tài khoản gốc..." disabled={!isEditing}>
                {accounts?.map((account) => (
                  <Option key={account.id} value={account.id}>
                    {account.tenTaiKhoan} - {account.soDu} VND
                  </Option>
                ))}
              </Select>
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              label="Tài khoản phụ"
              name="taiKhoanPhu"
              dependencies={["taiKhoanGoc"]}
              rules={[
                ({ getFieldValue }) => ({
                  validator(_, value) {
                    if (!value || getFieldValue("taiKhoanGoc") !== value) {
                      return Promise.resolve();
                    }
                    return Promise.reject(
                      new Error(
                        "Tài khoản nhận không được trùng tài khoản gốc !"
                      )
                    );
                  },
                }),
              ]}
            >
              <Select placeholder="Chọn tài khoản phụ...." disabled={!isTwoAccTx}>
                <Option
                  key="0"
                  value={null}
                  placeholder="Chọn tài khoản phụ...."
                ></Option>
                {accounts?.map((account) => (
                  <Option key={account.id} value={account.id}>
                    {account.tenTaiKhoan} - {account.soDu} VND
                  </Option>
                ))}
              </Select>
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Thể loại"
              name="theLoai"
              rules={[{ required: true, message: "Chọn thể loại !" }]}
            >
              <Select 
                placeholder="Chọn thể loại...."
                onChange={(option) => checkIfInterAccTranfer(option)}
                disabled={!isEditing}>
                {types?.map((type) => (
                  <Option key={type.id} value={type.id}>
                    {type.tenTheLoai}
                  </Option>
                ))}
              </Select>
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              label="Số tiền giao dịch"
              name="tongTien"
              rules={[
                {
                  required: true,
                  message: "Vui lòng nhập chi phí giao dịch !",
                },
                {
                  validator: (_, value) => {
                    if (!value) {
                      return Promise.resolve();
                    }
                    const numericValue = Number(value);
                    if (value && !isNaN(numericValue) && numericValue > 0) {
                      return Promise.resolve();
                    }
                    return Promise.reject(
                      new Error("Số tiền giao dịch phải là số hợp lệ!")
                    );
                  },
                },
              ]}
            >
              <InputNumber
                  style={{ width: '100%' }}
                  formatter={(value) => `${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
                  parser={(value) => value.replace(/\$\s?|(,*)/g, '')}
                  min={0}
                  disabled={!isEditing}
              />
            </Form.Item>
          </Col>
        </Row>
        <Form.Item
          label="Ghi chú"
          name="ghiChu"
        >
          <TextArea row={4} placeholder="Ghi chú giao dịch...." disabled={!isEditing}/>
        </Form.Item>
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
        {/* 
        <div className="flex justify-center space-x-4">
              
            </div>
            */}
      </Form>
    </Modal>
  );
};
export default UpdateTransactionModal;
