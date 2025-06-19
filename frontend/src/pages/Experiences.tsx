import { useState } from "react";
import styles from "./Experiences.module.css";
import { CxOption, CxSelect } from "@computas/designsystem/select/react";
import { experienceTypeMap } from "../types/experienceTypes";
import { useExperiences } from "../hooks/useExperiences";
import { ExperienceCard } from "../components/experiences/ExperienceCard";

export default function Experiences() {
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const [selectedExperience, setSelectedExperience] = useState<string | null>(
    null
  );


  // TODO Oppgave 2.1 of 2.2: Håndter loading og error av erfaringer
  const { data: experiences, isLoading: isExperiencesLoading, error: experiencesError} = useExperiences();

  if (isExperiencesLoading) {
    return <div className={styles.loading}>Loading Experiences...</div>
  }

  // if (experiencesError) {
  //   return <div className={styles.error}>Error loading Experiences</div>
  // }

  if (!experiences || experiences.length === 0) {
    return <div className={styles.noExperiences}>No experiences found.</div>;
  }

  const handleSelectChange = (e: Event) => {
    const customEvent = e as CustomEvent;
    const selectedFilter = customEvent.detail.value;
    console.log(selectedFilter);
    // TODO Oppgave 5.1: Filtrer experiences etter type
    setSelectedExperience(selectedFilter || null);
  };

  const filteredExperiences = () => {
    const validTypes = Object.keys(experienceTypeMap).filter(
      (type) => type !== "other"
    );

    if (selectedExperience === "other") {
      return experiences.filter(
        (experience) => !validTypes.includes(experience.type.toLowerCase())
      );
    } else if (selectedExperience) {
      return experiences.filter(
        (experience) =>
          experience.type.toLowerCase() === selectedExperience.toLowerCase()
      );
    }
    return experiences;
  };

  if(isExperiencesLoading) {
    return (
      <div className={styles.loading}>Loading...</div>
    )
  }
  else if (experiencesError) {
    return (
      <div className={styles.container}>
          <div className={styles.loading}>Error...</div>
        </div>
    )
  } else {
    return (
      <div className={styles.container}>
        <div className={styles.select}>
          <label className="cx-form-field">
            <div className="cx-form-field__input-container">
              <CxSelect onChange={handleSelectChange}>
                <CxOption value="">Ingen filtrering</CxOption>
                {Object.entries(experienceTypeMap).map(([type, data]) => (
                  <CxOption key={type} value={type}>
                    {data.text}
                  </CxOption>
                ))}
              </CxSelect>
            </div>
          </label>
        </div>
        <div className={styles.experiences}>
          {/*TODO Oppgave 3.1: Vis alle erfaringene*/}
            {/* {experiences.map((experience) => (
              <ExperienceCard key ={experience.id} experience = {experience}/>
              // <p key={experience.id}>{experience.title}</p>
            ))} */}

          {/* TODO Oppgave 4.1: Sorter erfaringene*/}
            {filteredExperiences().sort((a, b) => (new Date(b.startDate).getTime() - new Date(a.startDate).getTime()
            )).map((experience) => (
               <ExperienceCard key ={experience.id} experience = {experience}/>
            ))}
        </div>
      </div>
    );
  }


}
