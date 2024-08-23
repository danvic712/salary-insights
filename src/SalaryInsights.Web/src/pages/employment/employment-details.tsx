import { useState, useEffect } from "react";
import { Button } from "@/components/ui/button";
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import {
  Sheet,
  SheetContent,
  SheetHeader,
  SheetTitle,
} from "@/components/ui/sheet";
import { ScrollArea } from "@/components/ui/scroll-area";
import {
  PlusIcon,
  Pencil2Icon,
  Cross2Icon,
  CalendarIcon,
} from "@radix-ui/react-icons";
import { motion, AnimatePresence } from "framer-motion";

interface RoleItem {
  id: number;
  title: string;
  startDate: string;
  endDate: string;
  description: string;
}

interface CompanyExperience {
  companyId: string;
  companyName: string;
  companyDescription: string;
  roles: RoleItem[];
}

interface EmploymentDetailsProps {
  companyId: string;
  isOpen: boolean;
  onSave: (data: CompanyExperience) => void;
  onClose: () => void;
}

export default function EmploymentDetails({
  companyId,
  isOpen,
  onSave,
  onClose,
}: EmploymentDetailsProps) {
  const [companyExperience, setCompanyExperience] = useState<CompanyExperience>(
    {
      companyId: "",
      companyName: "",
      companyDescription: "",
      roles: [],
    }
  );
  const [editMode, setEditMode] = useState(false);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [newRole, setNewRole] = useState<Omit<RoleItem, "id">>({
    title: "",
    startDate: "",
    endDate: "",
    description: "",
  });

  useEffect(() => {
    // Fetch company data based on companyId
    // This is a placeholder. Replace with actual API call
    if (companyId) {
      setCompanyExperience({
        companyId,
        companyName: "Tech Innovators Inc.",
        companyDescription:
          "A cutting-edge technology company specializing in AI-driven solutions for enterprise clients.",
        roles: [
          {
            id: 1,
            title: "Senior Software Architect",
            startDate: "2022",
            endDate: "Present",
            description:
              "Leading the design and implementation of scalable microservices architecture.",
          },
          {
            id: 2,
            title: "Full Stack Developer",
            startDate: "2020",
            endDate: "2022",
            description:
              "Developed and maintained core products using React, Node.js, and AWS.",
          },
        ],
      });
    }
  }, [companyId]);

  const handleEdit = (id: number) => {
    setEditingId(id);
    const roleToEdit = companyExperience.roles.find((role) => role.id === id);
    if (roleToEdit) {
      setNewRole({
        title: roleToEdit.title,
        startDate: roleToEdit.startDate,
        endDate: roleToEdit.endDate,
        description: roleToEdit.description,
      });
    }
  };

  const handleDelete = (id: number) => {
    setCompanyExperience((prev) => ({
      ...prev,
      roles: prev.roles.filter((role) => role.id !== id),
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (editingId) {
      setCompanyExperience((prev) => ({
        ...prev,
        roles: prev.roles.map((role) =>
          role.id === editingId ? { ...role, ...newRole } : role
        ),
      }));
      setEditingId(null);
    } else {
      setCompanyExperience((prev) => ({
        ...prev,
        roles: [...prev.roles, { id: Date.now(), ...newRole }],
      }));
    }
    setNewRole({ title: "", startDate: "", endDate: "", description: "" });
  };

  const handleCompanyInfoChange = (
    field: "companyName" | "companyDescription",
    value: string
  ) => {
    setCompanyExperience((prev) => ({ ...prev, [field]: value }));
  };

  const handleSave = () => {
    onSave(companyExperience);
    setEditMode(false);
  };

  return (
    <Sheet open={isOpen} onOpenChange={onClose}>
      <SheetContent
        side="right"
        className="w-[400px] sm:w-[600px] sm:max-w-[100vw] p-0 flex flex-col"
      >
        <div className="flex-none pl-10 pr-10 pt-6 pb-6 border-b">
          <SheetHeader className="relative">
            <SheetTitle className="text-3xl font-bold flex items-center">
              {editMode ? (
                <Input
                  value={companyExperience.companyName}
                  onChange={(e) =>
                    handleCompanyInfoChange("companyName", e.target.value)
                  }
                  className="text-3xl font-bold"
                />
              ) : (
                companyExperience.companyName
              )}
            </SheetTitle>
            {editMode ? (
              <Textarea
                value={companyExperience.companyDescription}
                onChange={(e) =>
                  handleCompanyInfoChange("companyDescription", e.target.value)
                }
                className="mt-2 text-muted-foreground"
                rows={3}
              />
            ) : (
              <p className="text-muted-foreground mt-2">
                {companyExperience.companyDescription}
              </p>
            )}
          </SheetHeader>
        </div>

        <ScrollArea className="flex-grow">
          <div className="pr-10 pl-10 space-y-6">
            <h3 className="text-lg font-semibold">Roles & Responsibilities</h3>
            <AnimatePresence mode="wait">
              {editMode ? (
                <motion.div
                  key="edit-mode"
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1 }}
                  exit={{ opacity: 0 }}
                  transition={{ duration: 0.2 }}
                  className="space-y-6"
                >
                  {companyExperience.roles.map((role) => (
                    <Card key={role.id} className="relative overflow-hidden">
                      <CardHeader>
                        <CardTitle className="text-xl pr-20">
                          {role.title}
                        </CardTitle>
                        <p className="text-sm text-muted-foreground">
                          {role.startDate} - {role.endDate}
                        </p>
                      </CardHeader>
                      <CardContent>
                        <p>{role.description}</p>
                        <div className="absolute top-4 right-4 space-x-2">
                          <Button
                            variant="outline"
                            size="icon"
                            onClick={() => handleEdit(role.id)}
                          >
                            <Pencil2Icon className="h-4 w-4" />
                          </Button>
                          <Button
                            variant="outline"
                            size="icon"
                            onClick={() => handleDelete(role.id)}
                          >
                            <Cross2Icon className="h-4 w-4" />
                          </Button>
                        </div>
                      </CardContent>
                    </Card>
                  ))}
                  <Card>
                    <CardHeader>
                      <CardTitle>
                        {editingId ? "Edit Role" : "Add New Role"}
                      </CardTitle>
                    </CardHeader>
                    <CardContent>
                      <form onSubmit={handleSubmit} className="space-y-4">
                        <div>
                          <Label htmlFor="title">Job Title</Label>
                          <Input
                            id="title"
                            value={newRole.title}
                            onChange={(e) =>
                              setNewRole({ ...newRole, title: e.target.value })
                            }
                            required
                          />
                        </div>
                        <div className="grid grid-cols-2 gap-4">
                          <div>
                            <Label htmlFor="startDate">Start Date</Label>
                            <Input
                              id="startDate"
                              value={newRole.startDate}
                              onChange={(e) =>
                                setNewRole({
                                  ...newRole,
                                  startDate: e.target.value,
                                })
                              }
                              required
                            />
                          </div>
                          <div>
                            <Label htmlFor="endDate">End Date</Label>
                            <Input
                              id="endDate"
                              value={newRole.endDate}
                              onChange={(e) =>
                                setNewRole({
                                  ...newRole,
                                  endDate: e.target.value,
                                })
                              }
                              placeholder="Present"
                            />
                          </div>
                        </div>
                        <div>
                          <Label htmlFor="description">Description</Label>
                          <Textarea
                            id="description"
                            value={newRole.description}
                            onChange={(e) =>
                              setNewRole({
                                ...newRole,
                                description: e.target.value,
                              })
                            }
                            required
                          />
                        </div>
                        <Button type="submit" className="w-full">
                          <PlusIcon className="mr-2 h-4 w-4" />
                          {editingId ? "Update" : "Add"} Role
                        </Button>
                      </form>
                    </CardContent>
                  </Card>
                </motion.div>
              ) : (
                <motion.div
                  key="view-mode"
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1 }}
                  exit={{ opacity: 0 }}
                  transition={{ duration: 0.2 }}
                  className="space-y-6"
                >
                  {companyExperience.roles.map((role, index) => (
                    <Card key={role.id} className="relative overflow-hidden">
                      <div className="absolute top-0 bottom-0 left-0 w-1 bg-primary" />
                      <CardHeader>
                        <CardTitle className="text-xl flex items-center gap-2">
                          <span className="w-8 h-8 rounded-full bg-primary/10 flex items-center justify-center text-primary font-bold">
                            {index + 1}
                          </span>
                          {role.title}
                        </CardTitle>
                        <p className="text-sm text-muted-foreground flex items-center gap-1">
                          <CalendarIcon className="w-4 h-4" />
                          {role.startDate} - {role.endDate}
                        </p>
                      </CardHeader>
                      <CardContent>
                        <p className="leading-relaxed">{role.description}</p>
                      </CardContent>
                    </Card>
                  ))}
                </motion.div>
              )}
            </AnimatePresence>
          </div>
        </ScrollArea>

        <div className="flex-none p-8 border-t">
          <Button
            onClick={editMode ? handleSave : () => setEditMode(true)}
            className="w-full"
            variant={editMode ? "secondary" : "default"}
          >
            {editMode ? "Save Changes" : "Edit Employment Details"}
          </Button>
        </div>
      </SheetContent>
    </Sheet>
  );
}
