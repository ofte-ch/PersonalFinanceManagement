import {
  Button,
  Form,
  Input,
  Modal,
  Select,
  Row,
  Col,
  message,
  DatePicker,
  InputNumber,
} from "antd";
import { Flex } from "antd";
import { useTransactionStore } from "~/stores/transactions/transactionStore";
import { useCreateTransaction } from "~/api/transactions/create-transaction";
import { useTypes } from "~/api/types/get-types";
import { getAllAccounts } from "~/api/accounts/get-accounts";
import { useState, useEffect } from "react";
import moment from "moment";

const { TextArea } = Input;
const CreateTransactionModal = () => {
  const [form] = Form.useForm();
  const { openCreateModal, setOpenCreateModal } = useTransactionStore();
  const [accounts, setAccounts] = useState([]);
  const [isTwoAccTx, setIsTwoAccTx] = useState(false);
  const [isIncome, setIsIncome] = useState(false);
  const [isExpense, setIsExpense] = useState(false);

  useEffect(() => {
    getAllAccounts().then((accounts) => setAccounts(accounts));
  }, []);

  // Loại giao dịch
  const { data: types } = useTypes();
  const specialType = types?.find(x => x.tenTheLoai === "Giao dịch giữa 2 tài khoản").id;
  // Loại giao dịch theo thu chi
  const [ transactionType, setTransactionType] = useState("");

  const checkIfInterAccTranfer = (typeOption) => {
    if(typeOption === specialType)
      setIsTwoAccTx(true);
    else{
      form.setFieldValue("taiKhoanPhu", null);
      setIsTwoAccTx(false);
    }
  }

  const mutation = useCreateTransaction({
    onSuccess: () => {
      form.resetFields();
      setOpenCreateModal(false);
      message.success("Tạo giao dịch mới thành công !");
    },
    onFinish: () => {
      message.error("Tạo giao dịch mới thất bại !!!");
    },
  });

  const onFinish = (values) => {
    const formattedValues = {
      tenGiaoDich: values.tenGiaoDich,
      ngayGiaoDich: moment().format("YYYY-MM-DD HH:mm:ss"),
      taiKhoanGoc: values.taiKhoanGoc,
      taiKhoanPhu: values.taiKhoanPhu ? values.taiKhoanPhu : null,
      theLoai: values.theLoai,
      tongTien: values.tongTien,
      ghiChu: values.ghiChu,
    };
    console.log(formattedValues);
    mutation.mutate(formattedValues);
  };
  return (
    <Modal
      title={"Tạo giao dịch "}
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
        initialValues={{
          ngayGiaoDich: moment(),
        }}
      >
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Tên giao dịch"
              name="tenGiaoDich"
              rules={[{ required: true, message: "Nhập tên giao dịch" }]}
            >
              <Input placeholder="Nhập tên giao dịch..." />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              label="Ngày giao dịch"
              name="ngayGiaoDich"
              rules={[{ required: true, message: "Chọn ngày giao dịch" }]}
            >
              <DatePicker
                showTime
                disabled
                format="YYYY-MM-DD"
                style={{ width: "100%" }}
              />
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Tài khoản gốc"
              name="taiKhoanGoc"
              rules={[{ required: true, message: "Chọn tài khoản gốc !" }]}
            >
              <Select placeholder="Chọn tài khoản gốc...">
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
                // { required: false, message: "Chọn tài khoản phụ !" },
                ({ getFieldValue }) => ({
                  validator(_, value) {
                    if (!value || getFieldValue("taiKhoanGoc") !== value) {
                      return Promise.resolve();
                    }
                    return Promise.reject(
                      new Error(
                        "Tài khoản phụ không được trùng tài khoản gốc !"
                      )
                    );
                  },
                }),
              ]}
            >
              <Select placeholder="Chọn tài khoản phụ...." disabled={!isTwoAccTx}>
                <Option key="0" value={null} placeholder="Chọn tài khoản phụ...."></Option>
                {accounts?.map((account) => (
                  <Option key={account.id} value={account.id}>
                    {account.tenTaiKhoan} - {account.soDu} VND
                  </Option>
                ))}
              </Select>
            </Form.Item>
          </Col>
        </Row>
        {/* Thu hoặc chi */}
        <Row>
          <Col span={12} style={{margin:"0 auto"}}>
            <Form.Item
                label="Loại giao dịch"
                name="loaiGiaoDich"
                rules={[{ required: true, message: "Chọn loại giao dịch !" }]}
              >
                <Select 
                  placeholder="Chọn loại giao dịch...."
                  onChange={(option) => {setTransactionType(option)
                    console.log(option);
                  }}
                  >
                    <Option key="thu" value="Thu">Thu</Option>
                    <Option key="chi" value="Chi">Chi</Option>
                    </Select>
              </Form.Item>
          </Col>
        </Row>
        {/* Thể loại giao dịch */}
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Thể loại"
              name="theLoai"
              rules={[
                { required: true, message: "Chọn thể loại !" },
                ({ getFieldValue }) => ({
                  validator(_, value) {
                    if (!value || getFieldValue("loaiGiaoDich") === "Thu" || getFieldValue("loaiGiaoDich") === "Chi") {
                      return Promise.resolve();
                    }
                    return Promise.reject(
                      new Error(
                        "Vui lòng chọn loại giao dịch trước !"
                      )
                    );
                  },
                }),
              ]}
            >
              <Select 
                placeholder="Chọn thể loại...."
                onChange={(option) => checkIfInterAccTranfer(option)}
                >
                {types && types
                  .filter(type => type.phanLoai === transactionType)
                  .map((type) => (
                    <Option key={type.id} value={type.id}>
                        {type.tenTheLoai}
                    </Option>
                ))}
              </Select>
            </Form.Item>
          </Col>
          {/* Tổng tiền giao dịch */}
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
                />
            </Form.Item>
          </Col>
        </Row>
        <Form.Item
          label="Ghi chú"
          name="ghiChu"
        >
          <TextArea row={4} placeholder="Ghi chú giao dịch...." />
        </Form.Item>
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
export default CreateTransactionModal;
