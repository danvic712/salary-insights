import { useState, useEffect, useMemo } from "react";
import * as dateFns from "date-fns";

import { Avatar, Form, Card, Col, Row, Button, Table } from "@douyinfe/semi-ui";

export default function Payroll() {
  const DAY = 24 * 60 * 60 * 1000;
  const figmaIconUrl =
    "https://lf3-static.bytednsdoc.com/obj/eden-cn/ptlz_zlp/ljhwZthlaukjlkulzlp/figma-icon.png";
  const pageSize = 10;

  const columns = [
    {
      title: "标题",
      dataIndex: "name",
      width: 400,
      render: (text, record, index) => {
        return (
          <div>
            <Avatar
              size="small"
              shape="square"
              src={figmaIconUrl}
              style={{ marginRight: 12 }}
            ></Avatar>
            {text}
          </div>
        );
      },
      filters: [
        {
          text: "Semi Design 设计稿",
          value: "Semi Design 设计稿",
        },
        {
          text: "Semi D2C 设计稿",
          value: "Semi D2C 设计稿",
        },
      ],
      onFilter: (value, record) => record.name.includes(value),
    },
    {
      title: "大小",
      dataIndex: "size",
      sorter: (a, b) => (a.size - b.size > 0 ? 1 : -1),
      render: (text) => `${text} KB`,
    },
    {
      title: "所有者",
      dataIndex: "owner",
      render: (text, record, index) => {
        return (
          <div>
            <Avatar
              size="small"
              color={record.avatarBg}
              style={{ marginRight: 4 }}
            >
              {typeof text === "string" && text.slice(0, 1)}
            </Avatar>
            {text}
          </div>
        );
      },
    },
    {
      title: "更新日期",
      dataIndex: "updateTime",
      sorter: (a, b) => (a.updateTime - b.updateTime > 0 ? 1 : -1),
      render: (value) => {
        return dateFns.format(new Date(value), "yyyy-MM-dd");
      },
    },
  ];

  const getData = () => {
    const data = [];
    for (let i = 0; i < 46; i++) {
      const isSemiDesign = i % 2 === 0;
      const randomNumber = (i * 1000) % 199;
      data.push({
        key: "" + i,
        name: isSemiDesign
          ? `Semi Design 设计稿${i}.fig`
          : `Semi D2C 设计稿${i}.fig`,
        owner: isSemiDesign ? "姜鹏志" : "郝宣",
        size: randomNumber,
        updateTime: new Date().valueOf() + randomNumber * DAY,
        avatarBg: isSemiDesign ? "grey" : "red",
      });
    }
    return data;
  };

  const data = getData();

  const [dataSource, setData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [currentPage, setPage] = useState(1);

  const fetchData = (currentPage = 1) => {
    setLoading(true);
    setPage(currentPage);
    return new Promise((res, rej) => {
      setTimeout(() => {
        const data = getData();
        let dataSource = data.slice(
          (currentPage - 1) * pageSize,
          currentPage * pageSize
        );
        res(dataSource);
      }, 300);
    }).then((dataSource) => {
      setLoading(false);
      setData(dataSource);
    });
  };

  const handlePageChange = (page) => {
    fetchData(page);
  };

  useEffect(() => {
    fetchData();
  }, []);

  const scroll = useMemo(() => ({ y: 470 }), []);

  return (
    <div style={{ margin: "16px" }}>
      <Row gutter={[16, 16]}>
        <Col>
          <Card title="Search">
            <Form
              labelPosition="inset"
              layout="horizontal"
              wrapperCol={{ span: 2 }}
            >
              <Form.DatePicker
                field="startDate"
                label="开始日期"
                type="month"
                initValue={new Date().setMonth(0)}
              ></Form.DatePicker>
              <Form.DatePicker
                field="endDate"
                label="结束日期"
                type="month"
                initValue={new Date()}
              ></Form.DatePicker>
              <Form.Select field="companyId" label="公司">
                <Form.Select.Option value="operate">运营</Form.Select.Option>
                <Form.Select.Option value="rd">开发</Form.Select.Option>
                <Form.Select.Option value="pm">产品</Form.Select.Option>
                <Form.Select.Option value="ued">设计</Form.Select.Option>
              </Form.Select>
              <Button type="primary">搜索</Button>
              <Button type="secondary" style={{ marginLeft: 16 }}>
                新增
              </Button>
            </Form>
          </Card>
        </Col>
      </Row>
      <Row gutter={[16, 16]}>
        <Col>
          <Card style={{ height: "77vh" }}>
            <Table
              columns={columns}
              dataSource={dataSource}
              pagination={{
                currentPage,
                pageSize: 10,
                total: data.length,
                onPageChange: handlePageChange,
              }}
              loading={loading}
              scroll={scroll}
            />
          </Card>
        </Col>
      </Row>
    </div>
  );
}
