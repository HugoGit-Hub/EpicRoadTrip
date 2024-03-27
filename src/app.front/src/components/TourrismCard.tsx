import Card from "./Card";
import InstutitionCard, { IInstitution } from "./InstutitionCard";
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "./ui/accordion";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "./ui/tabs";

interface ITourismCardProps {
  loisirDepart: any;
  loisirDestination: any;
  cityDepart: string;
  cityDestination: string;
  onChange?: (institutions: IInstitution[]) => void;
  selectedInstitutions?: IInstitution[];
}

function TourrismCard({
  loisirDepart,
  loisirDestination,
  cityDepart,
  cityDestination,
  onChange,
  selectedInstitutions = [],
}: ITourismCardProps) {
  const handleSelected = (institution: IInstitution) => {
    const index = selectedInstitutions.findIndex(
      (element) =>
        element.lat === institution.lat &&
        element.lng === element.lng &&
        institution.name === institution.name
    );
    console.log(index);
    const newSelectedInstitutions = [...selectedInstitutions];
    if (index != -1) {
      newSelectedInstitutions.splice(index, 1);
    } else {
      console.log(institution);
      newSelectedInstitutions.push(institution);
    }
    if (onChange) onChange(newSelectedInstitutions);
  };
  return (
    <Card>
      <Tabs defaultValue="hotel">
        <TabsList className={`grid w-full grid-cols-4`}>
          <TabsTrigger value={"hotel"}>Hôtels</TabsTrigger>
          <TabsTrigger value={"restaurant"}>Restaurants</TabsTrigger>
          <TabsTrigger value={"bar"}>Bars</TabsTrigger>
          <TabsTrigger value={"evenement"}>Evènements</TabsTrigger>
        </TabsList>
        <div className="p-4">
          <Accordion type="single" collapsible>
            <AccordionItem value="depart">
              <AccordionTrigger>{cityDepart}</AccordionTrigger>
              <AccordionContent>
                <TabsContent value="hotel">
                  {loisirDepart[2] &&
                    loisirDepart[2].map((institution: IInstitution) => (
                      <InstutitionCard
                        {...institution}
                        onSelected={
                          onChange && (() => handleSelected(institution))
                        }
                        selected={
                          selectedInstitutions.findIndex(
                            (element) =>
                              element.lat === institution.lat &&
                              element.lng === element.lng &&
                              institution.name === institution.name
                          ) !== -1
                        }
                      />
                    ))}
                </TabsContent>
                <TabsContent value="restaurant">
                  {loisirDepart[3] &&
                    loisirDepart[3].map((institution: IInstitution) => (
                      <InstutitionCard
                        {...institution}
                        onSelected={
                          onChange && (() => handleSelected(institution))
                        }
                        selected={
                          selectedInstitutions.findIndex(
                            (element) =>
                              element.lat === institution.lat &&
                              element.lng === element.lng &&
                              institution.name === institution.name
                          ) !== -1
                        }
                      />
                    ))}
                </TabsContent>
                <TabsContent value="bar">
                  {loisirDepart[0] &&
                    loisirDepart[0].map((institution: IInstitution) => (
                      <InstutitionCard
                        {...institution}
                        onSelected={
                          onChange && (() => handleSelected(institution))
                        }
                        selected={
                          selectedInstitutions.findIndex(
                            (element) =>
                              element.lat === institution.lat &&
                              element.lng === element.lng &&
                              institution.name === institution.name
                          ) !== -1
                        }
                      />
                    ))}
                </TabsContent>
                <TabsContent value="evenement">
                  {loisirDepart[1] &&
                    loisirDepart[1].map((institution: IInstitution) => (
                      <InstutitionCard
                        {...institution}
                        onSelected={
                          onChange && (() => handleSelected(institution))
                        }
                        selected={
                          selectedInstitutions.findIndex(
                            (element) =>
                              element.lat === institution.lat &&
                              element.lng === element.lng &&
                              institution.name === institution.name
                          ) !== -1
                        }
                      />
                    ))}
                </TabsContent>
              </AccordionContent>
            </AccordionItem>

            <AccordionItem value="destination">
              <AccordionTrigger>{cityDestination}</AccordionTrigger>
              <AccordionContent>
                <TabsContent value="hotel">
                  {loisirDestination[2] &&
                    loisirDestination[2].map((institution: IInstitution) => (
                      <InstutitionCard
                        {...institution}
                        onSelected={
                          onChange && (() => handleSelected(institution))
                        }
                        selected={
                          selectedInstitutions.findIndex(
                            (element) =>
                              element.lat === institution.lat &&
                              element.lng === element.lng &&
                              institution.name === institution.name
                          ) !== -1
                        }
                      />
                    ))}
                </TabsContent>
                <TabsContent value="restaurant">
                  {loisirDestination[3] &&
                    loisirDestination[3].map((institution: IInstitution) => (
                      <InstutitionCard
                        {...institution}
                        onSelected={
                          onChange && (() => handleSelected(institution))
                        }
                        selected={
                          selectedInstitutions.findIndex(
                            (element) =>
                              element.lat === institution.lat &&
                              element.lng === element.lng &&
                              institution.name === institution.name
                          ) !== -1
                        }
                      />
                    ))}
                </TabsContent>
                <TabsContent value="bar">
                  {loisirDestination[0] &&
                    loisirDestination[0].map((institution: IInstitution) => (
                      <InstutitionCard
                        {...institution}
                        onSelected={
                          onChange && (() => handleSelected(institution))
                        }
                        selected={
                          selectedInstitutions.findIndex(
                            (element) =>
                              element.lat === institution.lat &&
                              element.lng === element.lng &&
                              institution.name === institution.name
                          ) !== -1
                        }
                      />
                    ))}
                </TabsContent>
                <TabsContent value="evenement">
                  {loisirDestination[1] &&
                    loisirDestination[1].map((institution: IInstitution) => (
                      <InstutitionCard
                        {...institution}
                        onSelected={
                          onChange && (() => handleSelected(institution))
                        }
                        selected={
                          selectedInstitutions.findIndex(
                            (element) =>
                              element.lat === institution.lat &&
                              element.lng === element.lng &&
                              institution.name === institution.name
                          ) !== -1
                        }
                      />
                    ))}
                </TabsContent>
              </AccordionContent>
            </AccordionItem>
          </Accordion>
        </div>
      </Tabs>
    </Card>
  );
}

export default TourrismCard;
