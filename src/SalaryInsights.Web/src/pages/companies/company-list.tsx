import { DataTable } from "@/components/data-table/data-table";
import { Payment, columns } from "./columns";

function getData(): Payment[] {
  // Fetch data from your API here.
  return [
    {
      id: "m5gr84i9",
      amount: 316,
      status: "success",
      email: "ken99@yahoo.com",
    },
    {
      id: "3u1reuv4",
      amount: 242,
      status: "success",
      email: "Abe45@gmail.com",
    },
    {
      id: "derv1ws0",
      amount: 837,
      status: "processing",
      email: "Monserrat44@gmail.com",
    },
    {
      id: "5kma53ae",
      amount: 874,
      status: "success",
      email: "Silas22@gmail.com",
    },
    {
      id: "bhqecj42",
      amount: 721,
      status: "failed",
      email: "carmella@hotmail.com",
    },
    {
      id: "3u1reuv8",
      amount: 242,
      status: "success",
      email: "Abe45@gmail.com",
    },
    {
      id: "derv1ws3",
      amount: 837,
      status: "processing",
      email: "Monserrat44@gmail.com",
    },
    {
      id: "5kma53a1",
      amount: 874,
      status: "success",
      email: "Silas22@gmail.com",
    },
    {
      id: "bhqecj42",
      amount: 721,
      status: "failed",
      email: "carmella@hotmail.com",
    },
    {
      id: "derv1ws3",
      amount: 837,
      status: "processing",
      email: "Monserrat44@gmail.com",
    },
    {
      id: "5kma53a1",
      amount: 874,
      status: "success",
      email: "Silas22@gmail.com",
    },
    {
      id: "bhqecj42",
      amount: 721,
      status: "failed",
      email: "carmella@hotmail.com",
    },
  ];
}

export default function CompanyList() {
  const data = getData();

  return (
    <>
      <h1 className="text-3xl font-bold">Company</h1>
      <DataTable columns={columns} data={data} />
    </>
  );
}
