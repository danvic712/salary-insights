import { useState, useEffect, useMemo } from "react";
import axios from "axios";
import {
  Col,
  Row,
  Tree,
  Card,
  Table,
  Space,
  Button,
  SideSheet,
  Typography,
  Form,
  Modal,
  Tag,
} from "@douyinfe/semi-ui";
import { IconDelete, IconEdit, IconPlus } from "@douyinfe/semi-icons";

export default function Parameter() {
  const { Select, Input, TextArea } = Form;

  const [parameterTypes, setParameterTypes] = useState();

  useEffect(() => {
    getParameterTypes();
  }, []);

  async function getParameterTypes() {
    const response = await axios.get("/api/parameters/types");
    const data = response.data;
    setParameterTypes(data);
  }

  const [loading, setLoading] = useState(false);
  const [emptyText, setEmptyText] = useState("No Result");
  const [parameters, setParameters] = useState();

  useEffect(() => {
    getParameters();
  }, []);

  async function getParameters(parameterType, name) {
    if (!parameterType) {
      setEmptyText("Please selected a parameter first");
      setParameters([]);
      return;
    }

    setEmptyText("No Result");
    setLoading(true);

    const response = await axios.get(
      `/api/parameters/types/${parameterType}?name=${name}`
    );
    setLoading(false);
    setParameters(response.data);
  }

  const columns = [
    {
      title: "参数名称",
      dataIndex: "name",
    },
    {
      title: "参数描述",
      dataIndex: "description",
    },
    {
      title: "所有者",
      dataIndex: "owner",
    },
    {
      title: "更新日期",
      dataIndex: "updateTime",
    },
  ];

  const [selectedParameters, setSelectedParameters] = useState();
  const rowSelection = useMemo(
    () => ({
      onChange: (selectedRowKeys, selectedRows) => {
        console.log(selectedRows)
        setSelectedParameters(selectedRows);
      },
    }),
    []
  );

  const [formVisible, setFormVisible] = useState(false);
  const changeFormVisible = () => {
    setFormVisible(!formVisible);
  };

  const editParameter = () => {
    setFormVisible(true);
  };

  const [modalVisible, setModalVisible] = useState(false);
  const changeModalVisible = () => {
    setModalVisible(!modalVisible);
  };

  async function deleteParameters() {}

  return (
    <div style={{ margin: "16px" }}>
      <Row gutter={[16, 16]}>
        <Col span={6}>
          <Card style={{ height: "98vh" }}>
            <Tree
              treeData={parameterTypes}
              directory
              filterTreeNode
              showFilteredOnly
              onSelect={(key) => getParameters(key, "")}
            ></Tree>
          </Card>
        </Col>
        <Col span={18}>
          <Card
            title={
              <Space>
                <Button
                  theme="light"
                  type="primary"
                  icon={<IconPlus />}
                  onClick={changeFormVisible}
                >
                  新增
                </Button>
                <Button
                  theme="light"
                  type="secondary"
                  icon={<IconEdit />}
                  onClick={editParameter}
                  disabled={
                    !selectedParameters || selectedParameters.length != 1
                  }
                >
                  编辑
                </Button>
                <Button
                  theme="light"
                  type="danger"
                  icon={<IconDelete />}
                  onClick={changeModalVisible}
                  disabled={
                    !selectedParameters || selectedParameters.length <= 0
                  }
                >
                  删除
                </Button>
              </Space>
            }
            style={{ height: "98vh" }}
          >
            <Table
              columns={columns}
              rowKey={"id"}
              empty={emptyText}
              dataSource={parameters}
              loading={loading}
              pagination={false}
              rowSelection={rowSelection}
              bordered
            />
          </Card>
        </Col>
      </Row>

      <SideSheet
        title={<Typography.Title heading={4}>参数创建</Typography.Title>}
        headerStyle={{ borderBottom: "1px solid var(--semi-color-border)" }}
        bodyStyle={{ borderBottom: "1px solid var(--semi-color-border)" }}
        visible={formVisible}
        onCancel={changeFormVisible}
        footer={
          <Space style={{ display: "flex", justifyContent: "flex-end" }}>
            <Button htmlType="reset">重置</Button>
            <Button theme="solid" htmlType="submit">
              提交
            </Button>
          </Space>
        }
      >
        <Form>
          <Row>
            <Col span={24}>
              <Select
                field="parameterType"
                defaultValue={selectedParameters?.[0].parameterType}
                label={{ text: "参数类别", required: true }}
                filter
                style={{ width: "100%" }}
              >
                {parameterTypes?.map((item) => (
                  <Select.Option
                    key={item.key}
                    value={item.value}
                    label={item.label}
                  >
                    {item.label}
                  </Select.Option>
                ))}
              </Select>
            </Col>
          </Row>
          <Row>
            <Col span={24}>
              <Input
                field="name"
                initValue={selectedParameters?.[0].name}
                label={{ text: "参数名称", required: true }}
                maxLength={10}
                style={{ width: "100%" }}
                showClear
              />
            </Col>
          </Row>
          <Row>
            <Col span={24}>
              <TextArea
                field="description"
                initValue={selectedParameters?.[0].description}
                label={{ text: "参数描述" }}
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

      <Modal
        title="警告"
        visible={modalVisible}
        onCancel={changeModalVisible}
        onOk={deleteParameters}
        closeOnEsc={true}
      >
        确定删除以下的参数
        <br />
        <Space style={{ padding: "10px 0px" }}>
          {selectedParameters?.map((item) => (
            <Tag key={item.id} closable>
              {item.name}
            </Tag>
          ))}
        </Space>
      </Modal>
    </div>
  );
}
