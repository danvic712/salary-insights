import { DataTableColumnHeader } from "@/components/data-table/data-table-column-header";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import { ColumnDef } from "@tanstack/react-table";
import { PencilIcon, TrashIcon } from "lucide-react";

// This type is used to define the shape of our data.
// You can use a Zod schema here if you want.
export type Employment = {
  id: string;
  companyName: string;
  position: string;
  startDate: string;
  endDate: string;
  employmentType: "fullTime" | "partTime";
};

export const columns: ColumnDef<Employment>[] = [
  {
    id: "select",
    header: ({ table }) => (
      <Checkbox
        checked={
          table.getIsAllPageRowsSelected() ||
          (table.getIsSomePageRowsSelected() && "indeterminate")
        }
        onCheckedChange={(value) => table.toggleAllPageRowsSelected(!!value)}
        aria-label="Select all"
      />
    ),
    cell: ({ row }) => (
      <Checkbox
        checked={row.getIsSelected()}
        onCheckedChange={(value) => row.toggleSelected(!!value)}
        aria-label="Select row"
      />
    ),
    enableSorting: false,
    enableHiding: false,
  },
  {
    accessorKey: "companyName",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Company Name" />
    ),
  },
  {
    accessorKey: "position",
    header: "Position",
  },
  {
    header: "Duration",
    cell: ({ row }) => (
      <div>
        {row.original.startDate} - {row.original.endDate}
      </div>
    ),
  },
  {
    accessorKey: "employmentType",
    header: "Employment Type",
    cell: ({ row }) => {
      return (
        <Badge
          variant={
            row.original.employmentType === "fullTime" ? "default" : "secondary"
          }
        >
          {row.original.employmentType === "fullTime" ? "全职" : "兼职"}
        </Badge>
      );
    },
  },
  {
    id: "actions",
    enableHiding: false,
    cell: ({ row }) => {
      const employment = row.original;

      return (
        <div className="flex space-x-2">
          <Button variant="outline" size="sm">
            <PencilIcon className="h-4 w-4" />
          </Button>
          <Button variant="destructive" size="sm">
            <TrashIcon className="h-4 w-4" />
          </Button>
        </div>
      );
    },
  },
];
