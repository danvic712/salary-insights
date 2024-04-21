import { useState, useEffect, useMemo, useCallback } from "react";

import axios from "axios";
import { format } from "date-fns";

import {
  Form,
  Card,
  Col,
  Row,
  Button,
  Table,
  Space,
  Tag,
  Popconfirm,
  Notification,
} from "@douyinfe/semi-ui";
import {
  IconPlus,
  IconSearch,
  IconEdit,
  IconDelete,
} from "@douyinfe/semi-icons";

import CompanyForm from "./CompanyForm";

export default function CompanyList() {
  const [companyList, setCompanyList] = useState({
    companyName: "",
    currentPage: 1,
    pageSize: 15,
    startDate: null,
    endDate: null,
    loading: false,
    dataSource: [],
    totalCount: 0,
  });
  const [formVisible, setFormVisible] = useState(false);
  const [companyId, setCompanyId] = useState();

  const columns = [
    {
      title: "公司名称",
      dataIndex: "name",
      width: "340px",
    },
    {
      title: "开始日期",
      dataIndex: "startDateStr",
    },
    {
      title: "结束日期",
      dataIndex: "endDateStr",
    },
    {
      title: "在职状态",
      dataIndex: "currentlyEmployed",
      render: (text, record, index) => {
        var color = text ? "light-blue" : "amber";
        return <Tag color={color}>{text ? "在职" : "离职"}</Tag>;
      },
    },
    {
      title: "所有者",
      dataIndex: "userName",
    },
    {
      title: "更新时间",
      dataIndex: "dateModifiedStr",
    },
    {
      title: "",
      dataIndex: "operation",
      fixed: "right",
      render: (text, record) => {
        return (
          <Space>
            <Button
              icon={<IconEdit />}
              size="small"
              theme="borderless"
              onClick={() => upsertCompany(record.id)}
            ></Button>

            <Popconfirm
              title={`确定删除${record.name}？`}
              content="此修改将不可逆"
              okType="danger"
              position="leftBottom"
              onConfirm={() => handleDelete(record.id)}
            >
              <Button
                icon={<IconDelete />}
                type="danger"
                size="small"
                theme="borderless"
              ></Button>
            </Popconfirm>
          </Space>
        );
      },
    },
  ];

  const upsertCompany = (companyId) => {
    setCompanyId(companyId);
    changeFormVisible();
  };

  const handleDelete = async (id) => {
    try {
      const response = await axios.delete(`/api/companies/${id}`);

      const result = response.data;

      if (result.status) {
        Notification.success({
          content: "数据操作成功",
        });
        fetchCompanies();
      } else {
        Notification.error({ content: result.message });
      }
    } catch (error) {
      console.error("Error deleting data:", error);
    }
  };

  const fetchCompanies = useCallback(async () => {
    setCompanyList((prevCompanyList) => ({
      ...prevCompanyList,
      loading: true,
    }));

    const params = new URLSearchParams({
      name: name,
      page: companyList.currentPage,
    });

    if (companyList.startDate) {
      params.append("startDate", companyList.startDate);
    }

    if (companyList.endDate) {
      params.append("endDate", companyList.endDate);
    }

    const url = `/api/companies/query?${params.toString()}`;

    const response = await axios.get(url);

    const responseObj = response.data;

    setCompanyList((prevCompanyList) => ({
      ...prevCompanyList,
      loading: false,
      currentPage: companyList.currentPage,
      dataSource: responseObj.data,
      totalCount: responseObj.totalCount,
    }));
  }, []);

  const queryCompanies = (formValues) => {
    const startDate = formValues.startDate
      ? format(new Date(formValues.startDate), "yyyy-MM-dd")
      : null;
    const endDate = formValues.endDate
      ? format(new Date(formValues.endDate), "yyyy-MM-dd")
      : null;

    fetchCompanies(formValues.name, startDate, endDate, 1);
  };

  const handlePageChange = (page) => {
    fetchCompanies("", null, null, page);
  };

  useEffect(() => {
    fetchCompanies();
  }, [fetchCompanies]);

  const changeFormVisible = () => {
    setFormVisible(!formVisible);
    if (!formVisible) return;

    setCompanyId(null);
    fetchCompanies();
  };

  return (
    <div style={{ margin: "16px" }}>
      <Row gutter={[16, 16]}>
        <Col>
          <Card>
            <Form
              labelPosition="inset"
              layout="horizontal"
              wrapperCol={{ span: 2 }}
              onSubmit={(formValues) => queryCompanies(formValues)}
            >
              <Form.Input
                field="name"
                label="公司名称"
                initValue={companyList.companyName}
              ></Form.Input>
              <Form.DatePicker
                field="startDate"
                label="开始日期"
                initValue={companyList.startDate}
              ></Form.DatePicker>
              <Form.DatePicker
                field="endDate"
                label="结束日期"
                initValue={companyList.endDate}
              ></Form.DatePicker>
              <Space>
                <Button htmlType="submit" type="primary" icon={<IconSearch />}>
                  搜索
                </Button>
                <Button
                  theme="light"
                  type="secondary"
                  icon={<IconPlus />}
                  onClick={() => upsertCompany(null)}
                >
                  新增
                </Button>
              </Space>
            </Form>
          </Card>
        </Col>
      </Row>
      <Row gutter={[16, 16]}>
        <Col>
          <Card style={{ height: "87vh" }}>
            <Table
              columns={columns}
              dataSource={companyList.dataSource}
              pagination={{
                currentPage: companyList.currentPage,
                pageSize: companyList.pageSize,
                total: companyList.totalCount,
                onPageChange: handlePageChange,
              }}
              loading={companyList.loading}
            />
          </Card>
        </Col>
      </Row>

      <CompanyForm
        visible={formVisible}
        companyId={companyId}
        onCancel={changeFormVisible}
      />
    </div>
  );
}
