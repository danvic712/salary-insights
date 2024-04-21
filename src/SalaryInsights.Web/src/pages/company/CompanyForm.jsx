import { useRef, useState, useEffect } from "react";

import axios from "axios";

import {
  Button,
  Col,
  Form,
  Row,
  Space,
  SideSheet,
  Typography,
  Notification,
} from "@douyinfe/semi-ui";

export default function CompanyForm({ visible, companyId, onCancel }) {
  const { DatePicker, Input, TextArea, RadioGroup, Radio } = Form;

  const formRef = useRef();

  const [company, setCompnay] = useState(null);

  useEffect(() => {
    const fetchCompanyDetailsAsync = async () => {
      try {
        if (companyId) {
          const response = await axios.get(`/api/companies/${companyId}`);
          setCompnay(response.data);
        } else setCompnay(null);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchCompanyDetailsAsync();
  }, [companyId]);

  useEffect(() => {
    formRef.current?.setValues(company, { isOverride: true });
  }, [company]);

  const submitForm = () => {
    formRef.current
      .validate()
      .then(async (values) => {
        await saveData(companyId, values);
      })
      .catch((errors) => {
        console.log(errors);
      });
  };

  const saveData = async (id, data) => {
    try {
      const method = id ? "PUT" : "POST";

      if (id) data.id = id;

      const response = await axios({
        method: method,
        url: "/api/companies",
        data: data,
      });

      const result = response.data;

      if (result.status) {
        Notification.success({
          content: "数据操作成功",
        });
        onCancel();
      } else {
        Notification.error({ content: result.message });
      }
    } catch (error) {
      console.error("Error:", error);
      Notification.error({ content: error });
    }
  };

  return (
    <>
      <SideSheet
        title={<Typography.Title heading={4}>公司信息</Typography.Title>}
        headerStyle={{ borderBottom: "1px solid var(--semi-color-border)" }}
        bodyStyle={{ borderBottom: "1px solid var(--semi-color-border)" }}
        visible={visible}
        onCancel={onCancel}
        footer={
          <Space style={{ display: "flex", justifyContent: "flex-end" }}>
            <Button>重置</Button>
            <Button theme="solid" onClick={submitForm}>
              提交
            </Button>
          </Space>
        }
      >
        <Form
          initValues={company}
          getFormApi={(formApi) => (formRef.current = formApi)}
        >
          <Row>
            <Col span={24}>
              <Input
                field="name"
                label={{ text: "公司名称", required: true }}
                maxLength={50}
                showClear
                rules={[{ required: true }]}
              />
            </Col>
          </Row>
          <Row>
            <Col span={11}>
              <RadioGroup
                field="currentlyEmployed"
                label="是否在职"
                rules={[{ type: "boolean" }, { required: true }]}
              >
                <Radio value={true}>是</Radio>
                <Radio value={false}>否</Radio>
              </RadioGroup>
            </Col>
            <Col span={11} offset={2}>
              <RadioGroup
                field="fullTime"
                label="全职"
                rules={[{ type: "boolean" }, { required: true }]}
              >
                <Radio value={true}>全职</Radio>
                <Radio value={false}>兼职</Radio>
              </RadioGroup>
            </Col>
          </Row>
          <Row>
            <Col span={11}>
              <DatePicker field="startDate" label="开始日期"></DatePicker>
            </Col>
            <Col span={11} offset={2}>
              <DatePicker
                field="endDate"
                label="结束日期"
                position="bottomRight"
              ></DatePicker>
            </Col>
          </Row>
          <Row>
            <Col span={24}>
              <TextArea
                field="remark"
                label={{ text: "备注" }}
                style={{ width: "100%" }}
                maxCount={200}
                autosize
                showClear
                showCounter
              />
            </Col>
          </Row>
        </Form>
      </SideSheet>
    </>
  );
}
