import { useState, useEffect, useMemo } from "react";
import * as dateFns from "date-fns";

import { Col, Row, Tree, Card, Avatar, Table } from "@douyinfe/semi-ui";

export default function Parameter() {
  const treeData = [
    {
      label: "Asia",
      value: "Asia",
      key: "0",
      children: [
        {
          label: "China",
          value: "China",
          key: "0-0",
          children: [
            {
              label: "Beijing",
              value: "Beijing",
              key: "0-0-0",
            },
            {
              label: "Shanghai",
              value: "Shanghai",
              key: "0-0-1",
            },
          ],
        },
        {
          label: "Japan",
          value: "Japan",
          key: "0-1",
          children: [
            {
              label: "Osaka",
              value: "Osaka",
              key: "0-1-0",
            },
          ],
        },
      ],
    },
    {
      label: "North America",
      value: "North America",
      key: "1",
      children: [
        {
          label: "United States",
          value: "United States",
          key: "1-0",
        },
        {
          label: "Canada",
          value: "Canada",
          key: "1-1",
        },
      ],
    },
  ];

  const DAY = 24 * 60 * 60 * 1000;
  const figmaIconUrl =
    "https://lf3-static.bytednsdoc.com/obj/eden-cn/ptlz_zlp/ljhwZthlaukjlkulzlp/figma-icon.png";
  const pageSize = 15;

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

  const scroll = useMemo(() => ({ y: 600 }), []);

  return (
    <div style={{ margin: "16px" }}>
      <Row gutter={[16, 16]}>
        <Col span={6}>
          <Card style={{ height: "96vh" }}>
            <Tree treeData={treeData} directory filterTreeNode></Tree>
          </Card>
        </Col>
        <Col span={18}>
          <Card style={{ height: "96vh" }}>
            <Table
              columns={columns}
              dataSource={dataSource}
              pagination={{
                currentPage,
                pageSize: 15,
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
